using System;
using System.Threading;

using ZeroMQ;

namespace EmailQueuing
{
    static partial class Program
    {
        private static void startWorker(string requestText)
        {
            string endpoint = "tcp://127.0.0.1:5555";

            // Create
            using (var context = new ZContext())
            using (var requester = new ZSocket(context, ZSocketType.REQ))
            {
                // Connect
                requester.Connect(endpoint);

                Console.Write("Sending {0}…", requestText);

                // Send
                requester.Send(new ZFrame(requestText));

                // Receive
                using (ZFrame reply = requester.ReceiveFrame())
                {
                    Console.WriteLine(" Received: {0} {1}!", requestText, reply.ReadString());
                }
            }
        }

        public static void Main(string[] args)
        {
            while(1==1)
            {
                startWorker("Hello World!");
            }
        }
    }
}
