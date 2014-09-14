using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class NetSmtpClient : ISmtpClient
    {
        private readonly SmtpClient client;

        public NetSmtpClient()
        {
            //takes default from config file
            client = new SmtpClient();
        }
        public void Send(MailMessage message)
        {
           client.Send(message);
        }

        public string Host
        {
            get { return client.Host; }
        }

        public void Dispose()
        {
            if(client != null)
                client.Dispose();
        }
    }
}
