using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Security;
using CommonTools;
using WindowsPSControl;

namespace PowerShellTests
{
    class Program
    {
        static void Main(string[] args)
        {

            //var ComputerName = "sauron";
            //string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";

            //var creds = new PSCredential("Systems\\Administrador", "Y?D6d#b@".ToSecureString());
            //var connection = new WSManConnectionInfo(false, ComputerName, 5985, "/wsman",shellUri, creds);
            //using (var runspace = RunspaceFactory.CreateRunspace(connection))
            //using (var powershell = PowerShell.Create()) {
            //    runspace.Open();
            //    powershell.Runspace = runspace;
            //    var Command = "dir c:\\";
            //    powershell.AddScript(Command);
            //    var results = powershell.Invoke();
            //    results.Where(o => o != null).ToList().ForEach(output => Console.WriteLine(output.ToString()));
            //}
            //Assume we’re done
            ADControl.EC = new EspackConnection()
            {
                ServerName = "Sauron",
                UserName = "SYSTEMS\\Administrador",
                Password = "Y?D6d#b@"
            };
            var _res = ADControl.CheckUser("patato");

        }
    }
}
