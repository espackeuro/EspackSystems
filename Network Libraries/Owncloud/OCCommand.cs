using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTools;

namespace Owncloud
{
    public static class OCCommands
    {
        public static EspackCredentials Credentials { get; set; }
        public static async Task<bool> CheckUser(string user)
        {
            using (var ocCommand = new OCGetUsers(user))
            {
                try
                {
                    ocCommand.setCredentials(Credentials);
                    await ocCommand.sendRequest();
                    return (ocCommand.userList.Count >= 1);
                } catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public static async Task<List<string>> GetUserGroups(string user)
        {
            using (var ocCommand = new OCGetUserGroups(user))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                return (ocCommand.groupsList);
            }
        }
        public static async Task<bool> CheckUserGroup(string user, string group)
        {
            var groupList = await GetUserGroups(user);
            return (groupList.Contains(group));
        }
        public static async Task<bool> AddUserToGroup(string user, string group)
        {
            using (var ocCommand = new OCAddUserToGroup(user, group))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                return (ocCommand.status == "ok");
            }
        }

        public static async Task<List<string>> GetGroups(string group)
        {
            using (var ocCommand = new OCGetGroups(group))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                return (ocCommand.groupList);
            }
        }
        public static async Task<bool> CheckGroup(string group)
        {
            var groupList = await GetGroups(group);
            return (groupList.Contains(group));
        }

        public static async Task<bool> AddUser(string user,string password, string fullName , string groups)
        {
            using (var ocCommand = new OCAddUser(user,password))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            using (var ocCommand = new OCEditUser(user,"display",fullName))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            foreach (var group in groups.Split('|'))
            {
                bool result = await CheckGroup(group); //lets check the group exists
                if (!result)
                {
                    result = await AddGroup(group); //if it doesnt we create it
                    if (!result)
                        return false;
                } else
                {
                    result = await CheckUserGroup(user, group); //lets check if the user already belongs to the group
                    if (result)
                        break;
                }
                result = await AddUserToGroup(user, group);// if he doesnt, we add to the group
                if (!result)
                    return false;
            }
            return true;
        }
        public static async Task<bool> UppUser(string user, string password, string fullName, string groups)
        {
            using (var ocCommand = new OCGetUsers(user))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                if (!ocCommand.userList.Contains(user))
                    return false;
            }
            using (var ocCommand = new OCEditUser(user, "display", fullName))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            using (var ocCommand = new OCEditUser(user, "password", password))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                if (ocCommand.status != "ok")
                    return false;
            }
            foreach (var group in groups.Split('|'))
            {
                if (group.Trim()!="")
                {
                    bool result = await CheckGroup(group); //lets check the group exists
                    if (!result)
                    {
                        result = await AddGroup(group); //if it doesnt we create it
                        if (!result)
                            return false;
                    }
                    else
                    {
                        result = await CheckUserGroup(user, group); //lets check if the user already belongs to the group
                        if (result)
                            break;
                    }
                    result = await AddUserToGroup(user, group);// if he doesnt, we add to the group
                    if (!result)
                        return false;
                }

            }
            return true;
        }
        public static async Task<bool> AddGroup(string group)
        {
            using (var ocCommand = new OCAddGroup(group))
            {
                ocCommand.setCredentials(Credentials);
                await ocCommand.sendRequest();
                return (ocCommand.status == "ok");
            }
        }
    }
}
