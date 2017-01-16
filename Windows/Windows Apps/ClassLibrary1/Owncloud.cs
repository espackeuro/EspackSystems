using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Owncloud
{
    public static class OCServerValues
    {
        public static string serverName
        {
            get
            {
                return "owncloud.espackeuro.com";
            }
        }
        public static bool SSL
        {
            get
            {
                return true;
            }
        }
        public static string route
        {
            get
            {
                return "/ocs/v1.php/cloud/";
            }
        }
        public static string serverURL(string user, string password,string command="")
        {
            return (SSL == true ? "https://" : "http://") + user + ":" + password + "@" + serverName + route+command;
        }
        public static string serverURL(string command="")
        {
            return (SSL == true ? "https://" : "http://") + serverName + route + command;
        }
    }
    public class OCInstruction: IDisposable
    {
        protected string Instruction { get; set; }
        protected HttpMethod Method { get; set; }
        protected Dictionary<string, string> Parameters = new Dictionary<string, string>();
        protected NameValueCollection ParametersGet = HttpUtility.ParseQueryString(string.Empty);
        private string User;
        private string Password;
        private AuthenticationHeaderValue CredentialsHeader { get; set; }
        protected HttpResponseMessage response = new HttpResponseMessage();
        public string responseString;
        //public XmlDocument responseXml = new XmlDocument();
        public XDocument responseX = new XDocument();
        public string status
        {
            get
            {
                return responseX.Descendants("status").ToList()[0].Value;
                //return responseXml.GetElementsByTagName("status")[0].InnerText;
            }
        }
        public int statusCode
        {
            get
            {
                return Convert.ToInt32(responseX.Descendants("statuscode").ToList()[0].Value);
            }
        }
        public string data
        {
            get
            {
                return responseX.Descendants("data").ToList()[0].Value;
            }
        }
        public string message
        {
            get
            {
                return responseX.Descendants("message").ToList()[0].Value;
            }
        }
        public void setCredentials (string user, string password) 
        {
            User = user;
            Password = password;
            var byteArray = Encoding.ASCII.GetBytes(User+":"+Password);
            CredentialsHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
        public void addParameter (string key, string value)
        {
            ParametersGet[key] = value;
            Parameters.Add(key, value);
        }

        public async Task sendRequest()
        {
            
            using (var client = new HttpClient())
            {
                if (CredentialsHeader == null)
                {
                    var e = new Exception("Credentials not set");
                    throw e;
                }
                if (Method == null)
                {
                    var e = new Exception("Method not set");
                    throw e;
                }
                if (Instruction == null)
                {
                    var e = new Exception("Instruction not set");
                    throw e;
                }
                client.DefaultRequestHeaders.Authorization = CredentialsHeader;
                try
                {
                    if (Method == HttpMethod.Post || Method == HttpMethod.Put)
                    {
                        var content = new FormUrlEncodedContent(Parameters);
                        switch (Method.ToString())
                        {
                            case "POST":
                                response = await client.PostAsync(OCServerValues.serverURL(Instruction), content);
                                break;
                            case "PUT":
                                response = await client.PutAsync(OCServerValues.serverURL(Instruction), content);
                                break;
                        }
                    }
                    else if (Method == HttpMethod.Get)
                    {
                        response = await client.GetAsync(OCServerValues.serverURL(Instruction) + "?" + ParametersGet.ToString());
                    }
                    else
                    {
                        var e = new Exception("Method not supported");
                        throw e;
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                responseString = await response.Content.ReadAsStringAsync();
                responseX = XDocument.Parse(responseString);
                if (statusCode != 100)
                {
                    var e = new Exception(status);
                    throw e;
                }
            }
        }

        public void Dispose()
        {
            ((IDisposable)response).Dispose();
        }
    }
    public class OCAddUser:OCInstruction
    {
        public OCAddUser(string user,string password)
        {
            Instruction = "users";
            Method = HttpMethod.Post;
            addParameter("userid", user);
            addParameter("password", password);
        }
    }
    public class OCGetUsers:OCInstruction
    {
        public List<string> userList { get
            {
                var result = responseX.Descendants("element").Select(x => x.Value).ToList<string>();
                return result;
            }
        }
        public OCGetUsers(string searchValue="")
        {
            Instruction = "users";
            Method = HttpMethod.Get;
            if (searchValue != "")
            {
                addParameter("search", searchValue);
            }
        }
    }

    public class OCEditUser:OCInstruction
    {
        public OCEditUser(string user, string fieldToEdit, string value)
        {
            Instruction = "users/" + user;
            if (!new[] { "email", "quota", "display", "password" }.Contains(fieldToEdit))
            {
                var e = new Exception("Invalid field to edit.");
                throw e;
            }
            Method = HttpMethod.Put;
            addParameter("key", fieldToEdit);
            addParameter("value", value);
        }
    }
    public class OCGetUserGroups:OCInstruction
    {
        public OCGetUserGroups(string user)
        {
            Instruction = "users/" + user + "/groups";
            Method = HttpMethod.Get;
        }
            public List<string> groupsList
        {
            get
            {
                var result = responseX.Descendants("element").Select(x => x.Value).ToList<string>();
                return result;
            }
        }
    }
    public class OCAddUserToGroup:OCInstruction
    {
        public OCAddUserToGroup(string user,string group)
        {
            Instruction = "users/" + user + "/groups";
            Method = HttpMethod.Post;
            addParameter("groupid", group);
        }
    }
    public class OCAddGroup:OCInstruction
    {
        public OCAddGroup(string group)
        {
            Instruction = "groups";
            Method = HttpMethod.Post;
            addParameter("groupid", group);
        }
    }
    public class OCGetGroupUsers:OCInstruction
    {
        public OCGetGroupUsers(string group)
        {
            Instruction = "groups/" + group;
            Method = HttpMethod.Get;
        }
        public List<string> userList
        {
            get
            {
                var result = responseX.Descendants("element").Select(x => x.Value).ToList<string>();
                return result;
            }
        }
    }
    public class OCGetGroups:OCInstruction
    {
        public OCGetGroups(string searchValue = "")
        {
            Instruction = "groups";
            Method = HttpMethod.Get;
            if (searchValue != "")
            {
                addParameter("search", searchValue);
            }
        }
            public List<string> groupList
        {
            get
            {
                var result = responseX.Descendants("element").Select(x => x.Value).ToList<string>();
                return result;
            }
        }
    }



}
