using Newtonsoft.Json.Linq;
using System;
using System.Threading;

using ZeroMQ;

namespace EmailQueuing
{
    static partial class Program
    {
        private static JObject sendJson()
        {
            JArray receivers = new JArray();
            receivers.Add("nic.caballero@alumnos.duoc.cl");
            receivers.Add("ncaballero@masaval.cl");

            JObject email = new JObject(
                new JProperty("subject", "Mi primer correo"),
                new JProperty("body", "Espero haya llegado el correo"),
                new JProperty("receivers", receivers)
            );                                   
            return email;
        }

        private static void startWorker(JObject request)
        {
            string requestText = request.ToString(Newtonsoft.Json.Formatting.None);
            string endpoint = "tcp://127.0.0.1:5555";

            using (var context = new ZContext())
            using (var requester = new ZSocket(context, ZSocketType.REQ))
            {
                requester.Connect(endpoint);

                Console.Write("Sending {0}…", requestText);
                requester.Send(new ZFrame(requestText));
                using (ZFrame reply = requester.ReceiveFrame())
                {
                    Console.WriteLine(" Received: {0} {1}!", requestText, reply.ReadString());
                }
            }
        }

        public static void Main(string[] args)
        {
            startWorker(sendJson());
        }
    }
}
