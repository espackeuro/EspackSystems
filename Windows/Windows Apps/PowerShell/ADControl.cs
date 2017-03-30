using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Security;
using CommonTools;


namespace WindowsPSControl
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

                        //var task = powershell.BeginInvoke();
                        //Results = powershell.EndInvoke(task);
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
    public static class ADControl 
    {
        public static EspackDomainConnection EC { get; set; } = new EspackDomainConnection();
        public static string Results { get; set; }
        public static async Task<bool> CreateUser(string Name, string Surname, string UserCode, string Password)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format("New-ADUser -Name '{0} {1}' -GivenName {0} -Surname {1} -SamAccountName {2} -UserPrincipalName {2}@systems.espackeuro.com -PasswordNeverExpires:$True -AccountPassword (ConvertTo-SecureString -AsPlainText '{3}' -Force) -PassThru | Enable-ADAccount;", Name, Surname, UserCode, Password)
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return _res;
        }

        public static async Task<bool> UpdateUser(string Name, string Surname, string UserCode, string Password)
        {
            var command = new PowerShellCommand()
            {
                EC = EC,
                Command = string.Format(@"
Get-ADUser -Identity '{0}' | Rename-ADObject -NewName '{1} {2}' ;
Get-ADUser -Identity '{0}' | Set-ADUser -DisplayName '{1} {2}' -GivenName '{1}' -Surname '{2}' -PasswordNeverExpires:$True;
Get-ADUser -Identity '{0}' | Set-ADAccountPassword -Reset -NewPassword (ConvertTo-SecureString -AsPlainText '{3}' –Force);
", UserCode, Name, Surname, Password)
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
                Command = "Get-ADUser -Filter {sAMAccountName -eq '"+UserCode+"'}"
            };
            var _res = await command.InvokeAsync();
            Results = command.SResults;
            return Results != "";
        }
    }
}
