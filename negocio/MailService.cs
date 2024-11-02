using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace negocio
{
    public class MailService
    {
        private static readonly string fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        private static readonly string fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
        private static readonly string fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
        private static readonly string smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        private static readonly string smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

        private MailService() {
            
        }

        public static void sendMail(string email, string nombreCompleto, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage(
                    new MailAddress(
                        fromEmailAddress,
                        fromEmailDisplayName
                        ),
                    new MailAddress(email, nombreCompleto));

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = true;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al enviar email."));
            }
        }
    }
}
