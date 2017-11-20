using System;
using System.Collections.Generic;
using ZeroMQ;

namespace EmailQueuing
{
    public class Worker
    {
        private const string endpoint = "tcp://127.0.0.1:5555";

        public void startWorker(string requestText)
        {
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
    }
}
