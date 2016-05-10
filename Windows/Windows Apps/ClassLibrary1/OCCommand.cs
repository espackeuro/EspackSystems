using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owncloud
{
    public static class OCCommands
    {
        public static async Task<bool> CheckUser(string username, string masterPassword)
        {
            using (var ocCommand = new OCGetUsers(username))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.status == "OK");
            }

        }
    }
}
