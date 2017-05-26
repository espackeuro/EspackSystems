using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADControl;
using BaseService;
using CommonTools;

namespace ADService
{
    class ADServiceClass : ISyncedService
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

        public string ServiceName { get { return "DOMAIN"; } }

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
            if (!await AD.CheckGroup(_localGroup))
            {
                var _Path = OU != "" ? string.Format("OU={0},{1}", OU, AD.DefaultPath) : AD.DefaultPath;
                await AD.CreateGroup(_localGroup, string.Format("{0} group", _localGroup), "Security", _Path);
                //await ADControlClass.MoveGroupToOU(_localGroup, OU);
            }
            await AD.AddUserToGroup(UserCode, _localGroup);
            if (!await AD.CheckGroup(Group))
            {
                await AD.CreateGroup(Group, string.Format("{0} group", Group), "Security", Values.DefaultPath);
            }
            await AD.AddUserToGroup(UserCode, Group);
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
                if (User.flags != null)
                {
                    if (User.flags.Contains("NEXTCLOUD"))
                    {
                        await AD.AddUserToGroup(User.UserCode, "nextCloud");
                    }
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
    }
}
