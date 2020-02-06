using Database.Context;
using DStore.Helpers;
using DStore.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DStore.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string pass, string name, DatabaseContext _context, AppSettings _appSettings, bool create = true)
        {
            try
            {
                MailAddress from = new MailAddress(_emailSettings.SenderEmail, "Itaú Fábrica de Emails", Encoding.UTF8);
                MailAddress to = new MailAddress(email, email, Encoding.UTF8);
                MailMessage msg = new MailMessage(from, to);

                msg.IsBodyHtml = true;

                //if (create)
                //{
                //    msg.Body = string.Format(@"<!DOCTYPE html><html lang='en'><body><div style='max-width: 500px; margin: 0 auto;'>" +
                //                    "<h3>Olá {0}!</h3><p>Segue seus dados de acesso a ferramenta:<br />Usuário: <strong>{2}</strong><br />" +
                //                    "Senha:<strong>{1}</strong><br />Endereço da ferramenta:<a href='{3}' target='_blank'>{3}</a><br /></p><br />" +
                //                    "<p>Lembrando que esta é uma seneha gerada automaticamente pelo sistema e deve ser trocada assim que realizar o primeiro login.</p></div></body></html>",
                //        name, pass, email, _appSettings.SiteHost);
                //}
                //else
                //{
                    msg.Body = string.Format(@"<!DOCTYPE html><html lang='en'><body><div style='max-width: 500px; margin: 0 auto;'>" +
                        "<h3>Olá {0}!</h3><p>Sua nova senha para acessar o sitema do Gerador de Email é: <strong>{1}</strong></p><br>" +
                        "<p>Lembrando que esta é uma seneha gerada automaticamente pelo sistema e deve ser trocada assim que realizar o próximo login.</p></div></body></html>",
                        name, pass);
                //}

                msg.Subject = subject;
                msg.BodyEncoding = Encoding.UTF8;
                msg.SubjectEncoding = Encoding.UTF8;

                SmtpClient client = new SmtpClient(_emailSettings.MailServer);

                client.EnableSsl = true;
                client.Port = _emailSettings.MailPort;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                client.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                //ErrorStack error = new ErrorStack(ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty, string.Format("{0}: {1}", "EmailSender", "SendEmailAsync"), 0);
                //await _context.Error.AddAsync(error);
                //await _context.SaveChangesAsync();

                return false;
            }
        }
    }
}
