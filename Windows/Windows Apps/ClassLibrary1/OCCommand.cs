using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owncloud
{
    public static class OCCommands
    {
        public static async Task<bool> CheckUser(string user, string masterPassword)
        {
            using (var ocCommand = new OCGetUsers(user))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.userList.Count == 1);
            }

        }
        public static async Task<List<string>> GetUserGroups(string user, string masterPassword)
        {
            using (var ocCommand = new OCGetUserGroups(user))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.groupsList);
            }
        }
        public static async Task<bool> CheckUserGroup(string user, string group, string masterPassword)
        {
            var groupList = await GetUserGroups(user, masterPassword);
            return (groupList.Contains(group));
        }
        public static async Task<bool> AddUserToGroup(string user, string group, string masterPassword)
        {
            using (var ocCommand = new OCAddUserToGroup(user, group))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.status == "ok");
            }
        }

        public static async Task<List<string>> GetGroups(string group, string masterPassword)
        {
            using (var ocCommand = new OCGetGroups(group))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.groupList);
            }
        }
        public static async Task<bool> CheckGroup(string group, string masterPassword)
        {
            var groupList = await GetGroups(group, masterPassword);
            return (groupList.Contains(group));
        }

        public static async Task<bool> AddUser(string user,string password, string fullName , string groups,string masterPassword)
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
                if (ocCommand.status != "ok")
                    return false;
            }
            foreach (var group in groups.Split('|'))
            {
                bool result = await CheckGroup(group,masterPassword); //lets check the group exists
                if (!result)
                {
                    result = await AddGroup(group, masterPassword); //if it doesnt we create it
                    if (!result)
                        return false;
                } else
                {
                    result = await CheckUserGroup(user, group, masterPassword); //lets check if the user already belongs to the group
                    if (result)
                        break;
                }
                result = await AddUserToGroup(user, group, masterPassword);// if he doesnt, we add to the group
                if (!result)
                    return false;
            }
            return true;
        }
        public static async Task<bool> UppUser(string user, string password, string fullName, string groups, string masterPassword)
        {
            using (var ocCommand = new OCGetUsers(user))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                if (!ocCommand.userList.Contains(user))
                    return false;
            }
            using (var ocCommand = new OCEditUser(user, "display", fullName))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            using (var ocCommand = new OCEditUser(user, "password", password))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            foreach (var group in groups.Split('|'))
            {
                if (group.Trim()!="")
                {
                    bool result = await CheckGroup(group, masterPassword); //lets check the group exists
                    if (!result)
                    {
                        result = await AddGroup(group, masterPassword); //if it doesnt we create it
                        if (!result)
                            return false;
                    }
                    else
                    {
                        result = await CheckUserGroup(user, group, masterPassword); //lets check if the user already belongs to the group
                        if (result)
                            break;
                    }
                    result = await AddUserToGroup(user, group, masterPassword);// if he doesnt, we add to the group
                    if (!result)
                        return false;
                }

            }
            return true;
        }
        public static async Task<bool> AddGroup(string group,string masterPassword)
        {
            using (var ocCommand = new OCAddGroup(group))
            {
                ocCommand.setCredentials("admin", masterPassword);
                await ocCommand.sendRequest();
                return (ocCommand.status == "ok");
            }
        }
    }
}
