﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADControl;
using BaseService;
using CommonTools;
using System.Diagnostics;

namespace ADService
{
    public class ADServiceClass : ISyncedService
    {
        public EspackCredentials ServiceCredentials
        {
            get
            {
                return new EspackCredentials()
                {
                    User = AD.EC.UserName,
                    Password = AD.EC.Password.ToSecureString()
                };
            }
            set
            {
                AD.EC.UserName = value.User;
                AD.EC.Password = value.Password.ToUnsecureString();
            }
        }
        public string ServerName { get; set; }
        public Dictionary<string,string> Flags { get; set; }
        public string ServiceName { get { return "DOMAIN"; } }
        public string[] Domains = new string[] { "espackeuro.com", "grupointerpack.com" };

        //public async Task<bool> CheckExist(string UserCode)
        //{
        //    return await AD.CheckUser(UserCode);
        //}

        public Task<bool> Disable(string UserCode)
        {
            throw new NotImplementedException();
        }

        private async Task AssignUserGroupOU(string UserCode, string Group, string OU, string OUDescription)
        {
            if (!await AD.CheckOrganizationalUnit(OU))
            {
                await AD.CreateOrganizationalUnit(OU, OUDescription, AD.DefaultPath);
            }
            await AD.MoveUserToOU(UserCode, OU);
            var _localGroup = string.Format("{0}.{1}", Group, OU);
            var _Path = OU != "" ? string.Format("OU={0},{1}", OU, AD.DefaultPath) : AD.DefaultPath;
            if (!await AD.CheckGroup(_localGroup, _Path))
            {
                //var _Path = OU != "" ? string.Format("OU={0},{1}", OU, AD.DefaultPath) : AD.DefaultPath;
                await AD.CreateGroup(_localGroup, _localGroup, "Security", _Path, new Dictionary<string, string> { { "mail", string.Format("{0}@espackeuro.com", _localGroup) } });
                //await ADControlClass.MoveGroupToOU(_localGroup, OU);
            }
            await AD.AddUserToGroup(UserCode, _localGroup, GroupPath: _Path);
            if (!await AD.CheckGroup(Group, AD.DefaultPath))
            {
                await AD.CreateGroup(Group, Group, "Security", AD.DefaultPath);
            }
            await AD.AddUserToGroup(UserCode, Group, GroupPath: AD.DefaultPath);
        }

        public async Task<bool> Insert(EspackUser User)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                if (!await AD.CheckUser(User.UserCode))
                {
                    await AD.CreateUser(User.Name, User.Surname, User.UserCode, User.Password, User.Email, User.Sede.COD3);
                    await AssignUserGroupOU(User.UserCode, User.Group, User.Sede.COD3, User.Sede.COD3Description);
                    return true;
                }
                else
                {
                    return false;
                    throw new Exception(string.Format("User {0} already exists.", User.UserCode));
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> Interact(EspackUser User)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                if (!await AD.CheckUser(User.UserCode))
                {
                    await AD.CreateUser(User.Name, User.Surname, User.UserCode, User.Password, User.Email, User.Sede.COD3);
                }
                else
                {
                    await AD.UpdateUser(User.Name, User.Surname, User.UserCode, User.Password, User.Email, User.Sede.COD3);
                }
                await AssignUserGroupOU(User.UserCode, User.Group, User.Sede.COD3, User.Sede.COD3Description);
                foreach (var f in Flags)
                {
                    if (!User.Flags.Contains(f.Key))
                        await AD.RemoveUserFromGroup(User.UserCode, f.Value, AD.DefaultPath);
                }
                if (User.Flags != null)
                {
                    foreach (var f in User.Flags)
                    {
                        if (!await AD.CheckGroup(Flags[f]))
                        //var _Path = OU != "" ? string.Format("OU={0},{1}", OU, AD.DefaultPath) : AD.DefaultPath;
                            await AD.CreateGroup(Flags[f], Flags[f], "Security", AD.DefaultPath);
                        await AD.AddUserToGroup(User.UserCode, Flags[f], GroupPath: AD.DefaultPath);
                        //await ADControlClass.MoveGroupToOU(_localGroup, OU);
                    }


                    //if (User.Flags.Contains("NEXTCLOUD"))
                    //{
                    //    await AD.AddUserToGroup(User.UserCode, "NextCloud Users", GroupPath: AD.DefaultPath);
                    //}
                }
                if (User.Aliases != null)
                {
                    await AD.PropertyAdd(User.UserCode, "proxyAddresses", string.Join(",", User.Aliases), true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(EspackUser User)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                if (await AD.CheckUser(User.UserCode))
                {
                    await AD.UpdateUser(User.Name, User.Surname, User.UserCode, User.Password, User.Email, User.Sede.COD3);
                    await AssignUserGroupOU(User.UserCode, User.Group, User.Sede.COD3, User.Sede.COD3Description);
                    return true;

                }
                else
                {
                    return false;
                    throw new Exception(string.Format("User {0} does not exists.", User.UserCode));
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

        public async Task<bool> InteractGroup(EspackGroup Group)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                //string _groupName = Group.GroupCode.Split('@')[0];
                if (!await AD.CheckGroup(Group.GroupCode,AD.DefaultPathAliases))
                {
                    await AD.CreateGroup(Group.GroupCode, Group.GroupCode, "distribution", AD.DefaultPathAliases, new Dictionary<string, string> { { "mail",Group.GroupMail} });
                    //await AssignUserGroupOU(User.UserCode, User.Group, User.Sede.COD3, User.Sede.COD3Description);
                } else
                {
                    await AD.PropertyAdd(Group.GroupCode, "mail", string.Format("'{0}'",Group.GroupMail), true);
                    await AD.CleanGroup(Group.GroupCode);
                }
                //else
                //{
                //    return false;
                //    throw new Exception(string.Format("Group {0} already exists.", Group.GroupCode));
                //}
                if (Group.GroupMembers.Count()!=0)
                {
                    
                    foreach (var _member in Group.GroupMembers)
                    {
                        var _memberName = _member.Split('@')[0];
                        bool _isContact = false;
                        //var _path = AD.DefaultPath;
                        if (!Domains.Contains(_member.Split('@')[1]))
                        {
                            _memberName = _member.ToUpper().Replace("@", "_AT_");
                            if (!await AD.CheckObject(_memberName, "contact", AD.DefaultPathContacts))
                                await AD.CreateObject(_memberName, "contact", AD.DefaultPathContacts, new Dictionary<string, string> { { "mail", _member } });
                            _isContact = true;
                        }
                        try
                        {
                            await AD.AddUserToGroup(_memberName, Group.GroupCode, _isContact);
                        } catch (Exception ex)
                        {
                           EventLog.WriteEntry("ESS",string.Format("Error adding user {0} to group {1}.\nError message was {2}", _memberName, Group.GroupCode, ex.Message), EventLogEntryType.Error);
                        }
                    }
                    
                    //await AD.PropertyAdd(Group.GroupCode, "proxyAddresses", string.Join(",", Group.GroupMembers), true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> UpdateGroup(EspackGroup Group)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                if (!await AD.CheckGroup(Group.GroupCode))
                {
                    return false;
                    throw new Exception(string.Format("Group {0} doesn't exist.", Group.GroupCode));
                }
                if (Group.GroupMembers.Count() != 0)
                {
                    await AD.PropertyAdd(Group.GroupCode, "proxyAddresses", string.Join(",", Group.GroupMembers), true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> InsertGroup(EspackGroup Group)
        {
            AD.EC.ServerName = ServerName;
            try
            {
                string _groupName = Group.GroupCode.Split('@')[0];
                if (_groupName == "")
                    _groupName = Group.GroupCode.Split('@')[1];
                if (!await AD.CheckGroup(Group.GroupCode))
                {
                    await AD.CreateGroup(Group.GroupCode, _groupName, "distribution", AD.DefaultPathAliases);
                    //await AssignUserGroupOU(User.UserCode, User.Group, User.Sede.COD3, User.Sede.COD3Description);
                }
                if (Group.GroupMembers.Count() != 0)
                {
                    await AD.PropertyAdd(Group.GroupCode, "proxyAddresses", string.Join(",", Group.GroupMembers), true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> DisableGroup(string GroupCode)
        {
            throw new NotImplementedException();
        }
    }
}
