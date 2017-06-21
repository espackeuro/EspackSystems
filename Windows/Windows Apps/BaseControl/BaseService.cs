using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CommonTools;

namespace BaseService
{
    public struct EspackUser
    {
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public EspackSede Sede { get; set; }
        public List<string> Flags { get; set; }
        public List<string> Aliases { get; set; }
    }

    public struct EspackGroup
    {
        public string GroupCode { get; set; }
        public string GroupMail { get; set; }
        public string[] GroupMembers { get; set; }
    }

    public struct EspackSede
    {
        public string COD3 { get; set; }
        public string COD3Description { get; set; }
    }

    public interface ISyncedService
    {
        EspackCredentials ServiceCredentials { get; set; }
        String ServerName { get; set; }
        string ServiceName { get; }
        Dictionary<string, string> Flags { get; set; }
        //Task<bool> CheckExist(string UserCode);
        Task<bool> Insert(EspackUser User);
        Task<bool> Update(EspackUser User);
        Task<bool> Interact(EspackUser User);
        Task<bool> Disable(string UserCode);
        Task<bool> InsertGroup(EspackGroup Group);
        Task<bool> UpdateGroup(EspackGroup Group);
        Task<bool> InteractGroup(EspackGroup Group);
        Task<bool> DisableGroup(string GroupCode);
        
        //string ErrorMessage { get; set; }
    }
}
