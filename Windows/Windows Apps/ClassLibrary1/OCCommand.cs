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
                return (ocCommand.userList.Count == 1);
            }

        }
        public static async Task<bool> AddUser(string user,string password, string fullName , string masterPassword)
        {
            using (var ocCommand = new OCAddUser(user,password))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            using (var ocCommand = new OCEditUser(user,"display",fullName))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.status == "ok");
            }
        }
    }
}
