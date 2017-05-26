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
        public static string Results { get; set; }
        public static async Task<bool> CreateUser(string Name, string Surname, string UserCode, string Password, string EmailAddress, string COD3)
        {
            var division = string.Format("{0}.{1}/{2}", COD3.ToLower(), EmailAddress.Substring(EmailAddress.IndexOf('@') + 1), UserCode);
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("New-ADUser -Name '{0} {1}' -GivenName '{0}' -Surname '{1}' -SamAccountName '{2}' -DisplayName '{1} {2}' -EmailAddress '{4}' -UserPrincipalName '{2}@systems.espackeuro.com' -Division '{5}' -PasswordNeverExpires:$True -AccountPassword (ConvertTo-SecureString -AsPlainText '{3}' -Force) -PassThru | Enable-ADAccount;", Name, Surname, UserCode, Password,EmailAddress, division)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return _res;
        }

        public static async Task<bool>  PropertyAdd(string UserCode, string PropertyName, string PropertyValue,bool CleanFirst)
        {
            string commandString = string.Format("$user = Get-ADUser -Identity '{0}'; ", UserCode);
            commandString += CleanFirst ? string.Format("Set-ADUser -Identity $user -Clear {0}; ", PropertyName) : "";
            commandString += string.Format("Set-ADUser -Identity $user -Add @{{{0}={1}}}; ", PropertyName, PropertyValue);
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
            var division = string.Format("{0}.{1}/{2}", COD3.ToLower(), EmailAddress.Substring(EmailAddress.IndexOf('@')+ 1), UserCode);
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"
Get-ADUser -Identity '{0}' | Rename-ADObject -NewName '{1} {2}' ;
Get-ADUser -Identity '{0}' | Set-ADUser -DisplayName '{1} {2}' -GivenName '{1}' -Surname '{2}' -EmailAddress '{4}' -Division '{5}' -PasswordNeverExpires:$True;
Get-ADUser -Identity '{0}' | Set-ADAccountPassword -Reset -NewPassword (ConvertTo-SecureString -AsPlainText '{3}' –Force);
", UserCode, Name, Surname, Password, EmailAddress, division)
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

        public static async Task<bool> CheckGroup(string GroupCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"Get-ADGroup -LDAPFilter '(SAMAccountName={0})'", GroupCode)
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

        public static async Task<bool> CreateGroup(string GroupCode, string GroupName,string GroupCategory, string Path) //= "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com")
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"New-ADGroup -Name '{1}' -SamAccountName '{0}' -GroupCategory {2} -GroupScope Global -DisplayName '{1}' -Path '{3}'", GroupCode, GroupName,GroupCategory,Path)
            };
            var _res = await command.InvokeAsync();
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


        public static async Task<bool> AddUserToGroup(string UserCode, string GroupCode)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("Add-ADGroupMember '{0}' '{1}' ", GroupCode, UserCode)
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
                Command = string.Format(@"Get-ADObject -LDAPFilter '(&(name = {0})(objectClass = {1}))' -SearchBase '{2}' -SearchScope OneLevel", ObjectName, ObjectType, Path)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            _res = Results != "";
            return _res;
        }
        public static async Task<bool> CreateObject(string ObjectName, string ObjectType, string Path = "OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com")
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"New-ADObject -Name '{0}' -Type '{1}' - Path '{2}'", ObjectName, ObjectType, Path)
            };
            var _res = await command.InvokeAsync();
            return _res;
        }
    }
}
