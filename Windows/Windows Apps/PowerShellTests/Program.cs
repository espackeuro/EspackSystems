using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Security;
using CommonTools;
using ADControl;

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
            AD.EC = new EspackDomainConnection()
            {
                ServerName = "Sauron",
                UserName = "SYSTEMS\\Administrador",
                Password = "Y?D6d#b@"
            };

            Task tarea = doChecks();
            tarea.Wait();
            
        }


        static async Task doChecks()
        {
            bool _res;// 
            //_res = await ADControl.CheckUser("jgallego");
            //_res = await ADControl.UpdateUser("Juana", "Gallego", "jgallego", "wales136");
            //_res = await ADControl.CheckGroup("patata");
            //_res = await ADControl.CreateGroup("nextCloud","NextCloud Users");
            //_res = await ADControl.AddUserToGroup("jgallego", "nextCloud");
            //_res = await ADControl.CheckOrganizationalUnit("GRA");
            //_res = await ADControl.CreateOrganizationalUnit("test", "OU De test");
            //_res = await AD.MoveUserToOU("tost", "test");
            _res = await AD.CreateObject("Test", "Contact", "CN=Contacts,OU=ESPACK,DC=SYSTEMS,DC=espackeuro,DC=com",  AttributeList: new Dictionary<string, string> { { "mail", "test@test.com" }, { "telephonenumber", "1234" } });
        }

    }
}
