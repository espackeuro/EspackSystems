using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;


namespace WebDAVClient.Helpers
{
    public class WebDAVException : ExternalException
    {
        private int httpCode=0;
        public WebDAVException()
        {
        }

        public WebDAVException(string message) 
            : base(message)
        {}

        public WebDAVException(string message, int hr) 
            : base(message, hr)
        {}

        public WebDAVException(string message, Exception innerException) 
            : base(message, innerException)
        {}

        public WebDAVException(int httpCode, string message, Exception innerException)
            : base(message, innerException)
        {
           this.httpCode = httpCode;
        }

        public WebDAVException(int httpCode, string message)
            : base(message)
        {
            this.httpCode = httpCode;
        }

        public WebDAVException(int httpCode, string message, int hr)
            : base(message, hr)
        {
            this.httpCode = httpCode;
        }

        protected WebDAVException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {}

        public override string ToString()
        {
            var s = string.Format("HttpStatusCode: {0}", httpCode);
            s += Environment.NewLine + string.Format("ErrorCode: {0}", ErrorCode);
            s += Environment.NewLine + string.Format("Message: {0}", Message);
            s += Environment.NewLine + base.ToString();

            return s;
        }
    }
}