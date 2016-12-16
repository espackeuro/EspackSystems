// Service.cs
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace Microsoft.ServiceModel.Samples.BasicWebProgramming
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet]
        string EchoWithGet(string s);

        [OperationContract]
        [WebInvoke]
        string EchoWithPost(string s);
    }
    public class Service : IService
    {
        public string EchoWithGet(string s)
        {
            return "You said " + s;
        }

        public string EchoWithPost(string s)
        {
            return "You said " + s;
        }
    }
    class Program
    {


        static void Main(string[] args)
        {
            // This string uses a function to prepend the computer name at run time.
            string addressHttp = String.Format(
                "https://{0}:8080/Calculator",
                System.Net.Dns.GetHostEntry("").HostName);

            WSHttpBinding b = new WSHttpBinding();
            b.Security.Mode = SecurityMode.Transport;
            b.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            // You must create an array of URI objects to have a base address.
            Uri a = new Uri(addressHttp);
            Uri[] baseAddresses = new Uri[] { a };

            // Create the ServiceHost. The service type (Calculator) is not
            // shown here.
            ServiceHost host = new ServiceHost(typeof(Service), baseAddresses);
            // Add an endpoint to the service. Insert the thumbprint of an X.509 
            // certificate found on your computer. 
            Type c = typeof(IService);
            host.AddServiceEndpoint(c, b, "");
            host.Credentials.ServiceCertificate.SetCertificate(
                StoreLocation.LocalMachine,
                StoreName.My,
                X509FindType.FindBySubjectName,
                "");

            //WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("http://localhost:8000/"));
            try
            {
                //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "");
                host.Open();
                using (ChannelFactory<IService> cf = new ChannelFactory<IService>(new WebHttpBinding(), "http://localhost:8000"))
                {
                    cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

                    IService channel = cf.CreateChannel();

                    string s;

                    Console.WriteLine("Calling EchoWithGet via HTTP GET: ");
                    s = channel.EchoWithGet("Hello, world");
                    Console.WriteLine("   Output: {0}", s);

                    Console.WriteLine("");
                    Console.WriteLine("This can also be accomplished by navigating to");
                    Console.WriteLine("http://localhost:8000/EchoWithGet?s=Hello, world!");
                    Console.WriteLine("in a web browser while this sample is running.");

                    Console.WriteLine("");

                    Console.WriteLine("Calling EchoWithPost via HTTP POST: ");
                    s = channel.EchoWithPost("Hello, world");
                    Console.WriteLine("   Output: {0}", s);
                    Console.WriteLine("");
                }

                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                host.Abort();
            }
        }
    }
}