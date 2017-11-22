using Newtonsoft.Json.Linq;
using Sender;
using System;
using System.Collections.Generic;
using System.Threading;
using ZeroMQ;

namespace EmailSender
{
    class Program
    {
        /// <summary>
        /// Gets message data from received json and create a object with it
        /// </summary>
        /// <param name="obj">Data obtained from socket</param>
        /// <returns>Message</returns>
        private static Message GetEmailDataFromJson(JObject obj)
        {            
            string subject = (string)obj["subject"];
            string body = (string)obj["body"];
            List<string> receivers = new List<string>();
            foreach(string receiver in obj["receivers"])
            {
                receivers.Add(receiver);
            }
            return new Message(subject, body, receivers);
        }

        /// <summary>
        /// Start a socket listening to port 5555 with ZMQ library
        /// </summary>
        /// <param name="args"></param>
        private static void runServer()
        {
            Console.WriteLine("Levantando servidor");

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                responder.Bind("tcp://*:5555");

                while (true)
                {
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        string json = request.ReadString();
                        JObject o = JObject.Parse(json);
                        Message message = GetEmailDataFromJson(o);
                        message.sendMail();
                        Thread.Sleep(1);
                        responder.Send(new ZFrame("OK"));
                    }
                }
            }        
        }

        static void Main(string[] args)
        {
            runServer();
        }
    }
}
