using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;

        }
        
        public async Task SendEmailAsync(Message message)

        {
            var mailmessage = CreateEmailMessage(message);
            await SendAsync(mailmessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Sender",_emailConfig.From));
            emailMessage.To.Add(new MailboxAddress("Reciver","info@sm-solutions.com.hr"));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h4 style='color:gray'>{0}</h4>", $"<h4>Ime: {message.Name}  <br> Mobitel: {message.Mob} <br> Email:  {message.Email}  <br></h4>" + message.Content) } ;
            return emailMessage;
    
        }
       
       private async Task SendAsync (MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("info@sm-solutions.com.hr");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }     
    }
}
