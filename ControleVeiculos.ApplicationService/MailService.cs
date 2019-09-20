using ControleVeiculos.Domain.Services;
using System.Net;
using System.Net.Mail;


namespace ControleVeiculos.ApplicationService
{
    public class MailService : BaseAppService, IMailService
    {
        public string Send(string email, string password, string mailFrom, string mailTo, string subject, string body, string attachment)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(email);
            mail.To.Add(mailTo);
            mail.CC.Add(mailFrom);
            mail.Subject = subject;
            mail.Body = body;

            if (!string.IsNullOrEmpty(attachment)) mail.Attachments.Add(new Attachment(@attachment));

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(email, password);
                smtp.Send(mail);
                smtp.SendCompleted += (s, e) =>
                {
                    smtp.Dispose();
                };
            }
            return null;
        }
    }
}
