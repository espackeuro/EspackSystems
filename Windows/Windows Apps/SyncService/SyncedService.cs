using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owncloud;
using System.Net;
using CommonTools;

namespace SyncService
{
    public interface ISyncedService
    {
        EspackCredentials ServiceCredentials { get; set; }
        String ServerName { get; set; }
        string ServiceName { get; }
        Task<bool> CheckExist(string UserCode);
        Task<bool> Insert(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3);
        Task<bool> Update(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3);
        Task<bool> Interact(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3);
        Task<bool> Disable(string UserCode);
        //string ErrorMessage { get; set; }
    }



    class NextCloudService : ISyncedService
    {
        public EspackCredentials ServiceCredentials { get; set; }
        public string ServiceName { get { return "NEXTCLOUD"; } }

        public string ServerName { get; set; }

        public async Task<bool> CheckExist(string UserCode)
        {
            return await OCCommands.CheckUser(UserCode);
        }

        public async Task<bool> Disable(string UserCode)
        {
            return true;
        }

        public async Task<bool> Insert(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3)
        {
            OCServerValues.serverName = ServerName;
            try
            {
                if (!await CheckExist(UserCode))
                {
                    await OCCommands.AddUser(UserCode, Password, string.Format("{0} {1}", Name, Surname), COD3);
                    return true;
      
                } else
                {
                    return false;
                    throw new Exception(string.Format("User {0} already exists.", UserCode));
                }
            } catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> Interact(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3)
        {
            OCServerValues.serverName = ServerName;
            try
            {
                if (!await CheckExist(UserCode))
                {
                    await OCCommands.AddUser(UserCode, Password, string.Format("{0} {1}", Name, Surname), COD3);
                    return true;

                }
                else
                {
                    await OCCommands.UppUser(UserCode, Password, string.Format("{0} {1}", Name, Surname), COD3);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public async Task<bool> Update(string UserCode, string Name, string Surname, string Group, string Password, string Email, string COD3)
        {
            OCServerValues.serverName = ServerName;
            try
            {
                if (!await CheckExist(UserCode))
                {
                    return false;
                    throw new Exception(string.Format("User {0} does not exists.", UserCode));
                }
                else
                {
                    await OCCommands.UppUser(UserCode, Password, string.Format("{0} {1}", Name, Surname), COD3);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        //public string ErrorMessage { get; set; }
    }
}
