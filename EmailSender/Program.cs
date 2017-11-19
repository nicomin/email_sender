using System.Collections.Generic;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> receivers = new List<string>();
            receivers.Add("nic.caballero@alumnos.duoc.cl");
            Sender.Message message = new Sender.Message("Correo de prueba", "Este es un mensaje de prueba", receivers);
            message.sendMail();
        }
    }
}
