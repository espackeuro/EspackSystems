using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Security;
using CommonTools;


namespace ADControl
{
    public class EspackDomainConnection
    {
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public PSCredential Credentials
        {
            get
            {
                return new PSCredential(UserName, Password.ToSecureString());
            }

        }
        public WSManConnectionInfo WSMan
        {
            get
            {
                string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";
                return new WSManConnectionInfo(false, ServerName, 5985, "/wsman", shellUri, Credentials);
            }
        }
    }

    public class PowerShellCommand
    {
        public EspackDomainConnection EC { get; set; }
        public string Command { get; set; }
        public PSDataCollection<PSObject> Results { get; private set; }
        public string SResults
        {
            get
            {
                string _res = "";
                Results.Where(o => o != null).ToList().ForEach(output => _res += output + "\n");
                return _res;
            }
        }
        public async Task<bool> InvokeAsync()
        {
            try
            {
                using (var runspace = RunspaceFactory.CreateRunspace(EC.WSMan))
                using (var powershell = PowerShell.Create())
                {
                    runspace.Open();
                    powershell.Runspace = runspace;
                    powershell.AddScript(Command);

                    Results = await Task.Factory.FromAsync(powershell.BeginInvoke(), pResult => powershell.EndInvoke(pResult));
                    if (powershell.HadErrors)
                    {
                        throw powershell.Streams.Error[0].Exception;
                    }
                    return true;

                }
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }
    }
    public static class AD
    {
        public static EspackDomainConnection EC { get; set; } = new EspackDomainConnection();
        public const string DefaultPath = "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com";
        public const string DefaultPathAliases = "CN=Distribution Lists,OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com";
        public const string DefaultPathContacts = "CN=Contacts,OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com";
        public static string Results { get; set; }
        public static async Task<bool> CreateUser(string Name, string Surname, string UserCode, string Password, string EmailAddress, string COD3)
        {
            var division = string.Format("{0}/{1}", EmailAddress.Substring(EmailAddress.IndexOf('@') + 1), UserCode);
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("New-ADUser -Name '{0} {1}' -GivenName '{0}' -Surname '{1}' -SamAccountName '{2}' -DisplayName '{0} {1}' -EmailAddress '{4}' -UserPrincipalName '{2}@systems.espackeuro.com' -Division '{5}' -PasswordNeverExpires:$True -AccountPassword (ConvertTo-SecureString -AsPlainText '{3}' -Force) -PassThru | Enable-ADAccount;", Name, Surname, UserCode, Password,EmailAddress, division)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return _res;
        }

        public static async Task<bool>  PropertyAdd(string Code, string PropertyName, string PropertyValue,bool CleanFirst)
        {
            string commandString = string.Format("$object = Get-ADObject -LDAPFilter '(SAMAccountName={0})'; ", Code);
            commandString += CleanFirst ? string.Format("Set-ADObject -Identity $object -Clear {0}; ", PropertyName) : "";
            commandString += string.Format("Set-ADObject -Identity $object -Add @{{{0}={1}}}; ", PropertyName, PropertyValue);
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = commandString
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return _res;
        }

        public static async Task<bool> UpdateUser(string Name, string Surname, string UserCode, string Password, string EmailAddress, string COD3)
        {
            var division = string.Format("{1}/{2}", COD3.ToLower(), EmailAddress.Substring(EmailAddress.IndexOf('@')+ 1), UserCode);
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"
Get-ADUser -Identity '{0}' | Rename-ADObject -NewName '{1} {2}' ;
Get-ADUser -Identity '{0}' | Set-ADUser -DisplayName '{1} {2}' -GivenName '{1}' -Surname '{2}' {4} -Division '{5}' -PasswordNeverExpires:$True;
Get-ADUser -Identity '{0}' | Set-ADAccountPassword -Reset -NewPassword (ConvertTo-SecureString -AsPlainText '{3}' –Force);
", UserCode, Name, Surname, Password, EmailAddress != "" ? "-EmailAddress " + EmailAddress : "", division)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return _res;
        }
        public static async Task<bool> CheckUser(string UserCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = "Get-ADUser -Filter {sAMAccountName -eq '" + UserCode + "'}"
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return Results != "";
        }

        public static async Task<bool> CheckGroup(string GroupCode, string Path = DefaultPath)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADGroup -LDAPFilter '(SAMAccountName={0})' -SearchBase '{1}'", GroupCode, Path)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            _res = Results != "";
            return _res;
        }

        public static async Task<bool> CheckOrganizationalUnit(string OUCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADOrganizationalUnit -LDAPFilter '(Name={0})'", OUCode)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            _res = Results != "";
            return _res;
        }

        public static async Task<bool> CreateGroup(string GroupCode, string GroupName,string GroupCategory, string Path, Dictionary<string, string> AttributeList = null) //= "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com")
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"New-ADGroup -Name '{1}' -SamAccountName '{0}' -GroupCategory {2} -GroupScope Global -DisplayName '{1}' -Path '{3}'", GroupCode, GroupName,GroupCategory,Path)
            };
            var _res = await command.InvokeAsync();

            if (AttributeList != null)
                _res = await UpdateObject(GroupCode, "group", Path, AttributeList);

            return _res;
        }



        public static async Task<bool> CreateOrganizationalUnit(string OUCode, string OUDescription, string Path)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"New-ADOrganizationalUnit -Name '{0}'  -DisplayName '{1}' -Description '{1}' -path '{2}'", OUCode, OUDescription, Path)
            };
            var _res = await command.InvokeAsync();
            return _res;
        }


        public static async Task<bool> AddUserToGroup(string UserCode, string GroupCode, bool isContact=false, string GroupPath=DefaultPathAliases)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("$dlGroup = [adsi]'LDAP://CN={0},{1}';$dlUser = Get-ADObject -Filter {{{3} -eq '{2}'}};$dlGroup.Member.Add($dlUser.DistinguishedName);$dlGroup.psbase.CommitChanges()", GroupCode, GroupPath, UserCode, isContact ? "Name": "SamAccountName")
            };
            return await command.InvokeAsync();
        }
        public static async Task<bool> RemoveUserFromGroup(string UserCode, string GroupCode, string GroupPath = DefaultPathAliases)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("Get-ADGroup -LDAPFilter '(SAMAccountName={0})' -SearchBase '{1}' | remove-adgroupmember -Member '{2}'  -Confirm:$false", GroupCode, GroupPath, UserCode)
            };
            return await command.InvokeAsync();
        }

        public static async Task<bool> MoveUserToOU(string UserCode, string OUCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADUser -Identity '{0}' | Move-ADObject -TargetPath 'OU={1},OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com' ", UserCode, OUCode)
            };
            return await command.InvokeAsync();
        }
        public static async Task<bool> MoveGroupToOU(string GroupCode, string OUCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADGroup '{0}' | Move-ADObject -TargetPath 'OU={1},OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com' ", GroupCode, OUCode)
            };
            return await command.InvokeAsync();
        }

        public static async Task<bool> CheckObject(string ObjectName, string ObjectType, string Path= "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com")
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADObject -Filter {{(Name -eq '{0}') -and (objectClass -eq '{1}')}} -SearchBase '{2}' -SearchScope OneLevel", ObjectName, ObjectType, Path)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            _res = Results != "";
            return _res;
        }
        public static async Task<bool> CreateObject(string ObjectName, string ObjectType, string Path = "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com", Dictionary<string,string> AttributeList=null)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"New-ADObject -Name '{0}' -Type '{1}' -Path '{2}';", ObjectName, ObjectType, Path)
            };
            var _res = await command.InvokeAsync();

            if (AttributeList != null)
                _res = await UpdateObject(ObjectName, ObjectType, Path, AttributeList);

            return _res;
        }
        public static async Task<bool> UpdateObject(string ObjectName, string ObjectType, string Path = "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com", Dictionary<string, string> AttributeList = null)
        {
            string _attributeListString = "";
            //string _attributeKeys = "";
            if (AttributeList != null)
            {
                _attributeListString = string.Join(";", AttributeList.Select(r => string.Format("{0}='{1}'", r.Key, r.Value)).ToArray());
                //_attributeKeys = string.Join(",", AttributeList.Select(r => r.Key).ToArray());
            }
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("Get-ADObject -LDAPFilter '(Name={0})' -SearchBase '{2}'| Set-ADObject -Add @{{{1}}};", ObjectName,  _attributeListString, Path)
            };
            var _res = await command.InvokeAsync();
            return _res;
        }
        public static async Task<bool> CleanGroup(string GroupName)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("Get-ADGroupMember '{0}'| ForEach-Object {{Remove-ADGroupMember '{0}' $_ -Confirm:$false}}", GroupName)
            };
            var _res = await command.InvokeAsync();
            return _res;
        }
    }
}
