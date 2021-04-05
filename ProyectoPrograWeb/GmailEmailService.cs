using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProyectoPrograWeb
{
    public class GmailEmailService : SmtpClient
    {
        public string UserName { get; set; }

        public GmailEmailService(string strServer, int strPort, bool bSSL, string strUser, string strPassword) :
            base(strServer, strPort)
        {
            this.UserName = strUser;
            this.EnableSsl = bSSL;
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(this.UserName, strPassword);
        }
    }
}
