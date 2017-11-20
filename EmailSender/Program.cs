using System;
using System.Collections.Generic;
using System.Threading;
using ZeroMQ;

namespace EmailSender
{
    class Program
    {
        private static void runServer(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: ./{0} HWServer [Name]", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine();
                Console.WriteLine("    Name   Your name. Default: World");
                Console.WriteLine();
                args = new string[] { "World" };
            }

            string name = args[0];

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:5555");

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        Console.WriteLine("Received {0} madafacka", request.ReadString());

                        // Do some work
                        Thread.Sleep(1);

                        // Send
                        responder.Send(new ZFrame(name));
                    }
                }
            }        
        }

        static void Main(string[] args)
        {
            List<string> receivers = new List<string>();
            receivers.Add("nic.caballero@alumnos.duoc.cl");
            Sender.Message message = new Sender.Message("Correo de prueba", "Este es un mensaje de prueba", receivers);
            //message.sendMail();
            runServer(args);
        }
    }
}
