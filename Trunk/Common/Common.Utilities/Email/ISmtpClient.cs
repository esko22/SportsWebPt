using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface ISmtpClient
    {
        void Send(MailMessage message);
        string Host { get; }
    }
}
