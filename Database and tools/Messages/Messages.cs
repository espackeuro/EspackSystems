

namespace Messages
{
    using Encryption;
    using Compression;
    using System;
    using System.Xml.Linq;
    using CommonTools;
    using System.Threading.Tasks;
    using System.Text;
    using SocketsClient;
    using System.Net;

    public enum DecoratorType { PRE, POST }
    public interface IMessage
    {
        string Debug { get; }
        string MsgIn { get; set; }
        string MsgOut { get; }
        string ProcessName { get; }
        Task<bool> process();
    }
    //decorator abstract
    public abstract class MessageDecorator: IMessage
    {
        protected IMessage m_base;
        protected string _msgIn = "";
        protected string _msgOut;
        public string Debug { get; protected set; } = "";
        public string MsgIn {
            get
            {
                return _msgIn;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("-{0}-------------------------------\nMsgIn={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgIn = value;
                }
                else _msgIn = "";
            }

        }
        public string MsgOut
        {
            get
            {
                return _msgOut;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("Process {0}\nMsgOut={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgOut = value;
                }
                else _msgOut = "";
            }

        }
        public string ProcessName { get; protected set; }
        public abstract Task<bool> process();
        public MessageDecorator(IMessage m)
        {
            m_base = m;
        }
    }
    //This is a decorator
    public class EncryptMessageInput : MessageDecorator
    {

        public override async Task<bool> process()
         {            // Return result value. Display it on the console.
            try
            {

                //MsgOut = await Task.Run(() => { return StringCipher.Encrypt(MsgIn); }); //lets execute the process
                MsgOut= StringCipher.Encrypt(MsgIn);
                m_base.MsgIn = MsgOut; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgOut = m_base.MsgOut;
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public EncryptMessageInput(IMessage m): base(m)
        {
            ProcessName = "ENCRYPTION";
        }
    }

    //This is a decorator
    public class DecryptMessageInput : MessageDecorator
    {

        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                MsgOut = StringCipher.Decrypt(MsgIn); //lets execute the process
                m_base.MsgIn = MsgOut; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgOut = m_base.MsgOut;
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public DecryptMessageInput(IMessage m) : base(m)
        {
            ProcessName = "DECRYPTION";
        }
    }

    //This is a decorator
    public class CompressMessageInput : MessageDecorator
    {
        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                MsgOut = MsgIn.Length < 860 ? MsgIn : Convert.ToBase64String(cComp.Zip(MsgIn)); 
                m_base.MsgIn = MsgOut; //assign my output to the intput of the decorated instance
                if (await m_base.process())
                {
                    Debug += m_base.Debug;
                    _msgOut = m_base.MsgOut;
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public CompressMessageInput(IMessage m) :base(m)
        {
            ProcessName = "COMPRESSION";
        }
    }

    //This is a decorator
    public class DecompressMessageInput : MessageDecorator
    {
        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                if (MsgIn.IsPossiblyGZippedString())
                {
                    MsgOut = cComp.Unzip(MsgIn);
                } else
                {
                    MsgOut = MsgIn;
                }
                m_base.MsgIn = MsgOut; //assign my output to the intput of the decorated instance
                if (await m_base.process())
                {
                    Debug += m_base.Debug;
                    _msgOut = m_base.MsgOut;
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public DecompressMessageInput(IMessage m) : base(m)
        {
            ProcessName = "DECOMPRESSION";
        }
    }


    public class EncryptMessageOutput : MessageDecorator
    {

        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                m_base.MsgIn = MsgIn; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgIn = m_base.MsgOut;
                    MsgOut = StringCipher.Encrypt(MsgIn); //lets execute the process
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public EncryptMessageOutput(IMessage m) : base(m)
        {
            ProcessName = "ENCRYPTION";
        }
    }

    //This is a decorator
    public class DecryptMessageOutput : MessageDecorator
    {

        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                m_base.MsgIn = MsgIn; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgIn = m_base.MsgOut;
                    MsgOut = StringCipher.Decrypt(MsgIn); //lets execute the process
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public DecryptMessageOutput(IMessage m) : base(m)
        {
            ProcessName = "DECRYPTION";
        }
    }

    //This is a decorator
    public class CompressMessageOutput : MessageDecorator
    {
        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {

                m_base.MsgIn = MsgIn; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgIn = m_base.MsgOut;
                    var _compressedMessage=Convert.ToBase64String(cComp.Zip(MsgIn));
                    if (MsgIn.Length < _compressedMessage.Length)
                    {
                        MsgOut = MsgIn;
                    }
                    else
                        MsgOut = _compressedMessage;
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public CompressMessageOutput(IMessage m) : base(m)
        {
            ProcessName = "COMPRESSION";
        }
    }

    //This is a decorator
    public class DecompressMessageOutput : MessageDecorator
    {
        public override async Task<bool> process()
        {            // Return result value. Display it on the console.
            try
            {
                m_base.MsgIn = MsgIn; //assign my output to the intput of the decorated instance
                if (await m_base.process()) //execute the process of the decorated instance
                {
                    Debug += m_base.Debug;
                    _msgIn = m_base.MsgOut;
                    if (MsgIn.IsPossiblyGZippedString())
                    {
                        MsgOut = cComp.Unzip(MsgIn);
                    }
                    else
                    {
                        MsgOut = MsgIn;
                    }
                    return true;
                }
                else
                {
                    Debug += m_base.Debug;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug += string.Format("ERROR: {0}", ex.Message);
                return false;
            }
        }
        public DecompressMessageOutput(IMessage m) : base(m)
        {
            ProcessName = "DECOMPRESSION";
        }
    }

    //Concrete message for transmission
    public class SocketMessage : IMessage
    {
        public IPAddress ServerIP { get; set; }
        public int Port { get; set; }
        protected string _msgIn = "";
        protected string _msgOut;
        public string Debug { get; protected set; } = "";
        public string MsgIn
        {
            get
            {
                return _msgIn;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("-{0}-------------------------------\nMsgIn={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgIn = value;
                }
                else _msgIn = "";
            }

        }
        public string MsgOut
        {
            get
            {
                return _msgOut;
            }
            set
            {
                if (value != null)
                {
                    Debug += string.Format("Process {0}\nMsgOut={1}\nSize={2} bytes\n", ProcessName, value, value.Length);
                    _msgOut = value;
                }
                else _msgOut = "";
            }

        }
        public string ProcessName { get; protected set; }
        public SocketMessage() 
        {
            ProcessName= "SOCKETS TRANSMISSION";
        }
        public async Task<bool> process()
        {
            try
            {
                var Socket = new AsynchronousSocketClient() { ServerIP = ServerIP, ServerPort = Port };
                MsgOut = await Socket.AsyncConversation(MsgIn);
                //var task = Socket.AsyncConversation(MsgIn);
                //task.Wait();
                //await task;
                //MsgOut = task.Result;

                //MsgOut = Socket.SyncConversation(MsgIn);
                return true;
            } catch (Exception ex)
            {
                MsgOut = "Error: " + ex.Message;
                return false;
            }
            
        }
    }
    //decorator composition
    public class TransmitEntcryptedCompressedMessage : IMessage
    {
        public string Debug { get; }
        public string MsgIn { get; set; }
        public string MsgOut { get; private set; }
        public string ProcessName { get; }
        public IPAddress ServerIP { get; set; }
        public int Port { get; set; }
        public TransmitEntcryptedCompressedMessage() 
        {
            ProcessName = "FULL TRANSMISSION";
        }
        public async Task<bool> process()
        {
            var message= new SocketMessage() { ServerIP = ServerIP, Port = Port};
            var em = new EncryptMessageInput(message);
            var ecm = new CompressMessageInput(em);
            var dsecm = new DecryptMessageOutput(ecm);
            var ddsecm = new DecompressMessageOutput(dsecm) { MsgIn = MsgIn };

            var _result = await ddsecm.process();
            MsgOut = ddsecm.MsgOut;
            return _result;
        }
    }
    // the instantiable class
    public class Message: IMessage
    {
        public string Debug { get; }
        public string MsgIn { get; set; }
        public string MsgOut { get; private set; }
        public string ProcessName { get; }
        public Task<bool> process()
        {
            MsgOut = MsgIn;
            return Task.FromResult<bool>(true);
        }
    }
}
