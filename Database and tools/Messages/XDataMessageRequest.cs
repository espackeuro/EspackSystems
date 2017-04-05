using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Messages
{
    public class XDataMessageRequest
    {
        //private XDocument _x;
        private XElement ActionDefinition { get; set; }
        private XElement ActionData { get; set; }
        private XElement SessionNumber { get; set; }
        public XDocument XMessage
        {
            get
            {
                if (ActionDefinition == null)
                    throw new Exception("Action not defined");
                if (ActionData == null)
                    throw new Exception("Data not defined");
                if (SessionNumber == null)
                    throw new Exception("SessionNumber not defined");
                var _root = new XDocument();
                _root.Add(new XElement("message"));
                _root.Root.Add(ActionDefinition);
                _root.Root.Add(ActionData);
                _root.Root.Add(SessionNumber);
                return _root;
            }
        }
        public void SetActionDefinition(string _action)
        {
            ActionDefinition = new XElement("ActionDefinition", _action);
        }
        public string Session
        {
            get
            {
                return SessionNumber == null ? null : SessionNumber.Value;
            }
        }
        public string Action
        {
            get
            {
                return ActionDefinition == null ? null : ActionDefinition.Value;
            }
        }
        public XElement Data
        {
            get
            {
                return ActionData;
            }
        }
        public void SetActionData(XElement _data)
        {
            if (_data.Name != "data")
                throw new Exception("Root name of element should be 'data'");
            ActionData = _data;
        }
        public void SetSession(string _sessionNumber)
        {
            SessionNumber = new XElement("SessionNumber", _sessionNumber);
        }
        public override string ToString()
        {
            return XMessage.ToString();
        }
        public XDataMessageRequest(string _message)
        {
            XDocument _x = XDocument.Parse(_message);
            ActionDefinition = new XElement(_x.Root.Element("ActionDefinition"));
            ActionData = new XElement(_x.Root.Element("data"));
            SessionNumber = new XElement(_x.Root.Element("SessionNumber"));
        }
        public XDataMessageRequest() { }




    }


}
