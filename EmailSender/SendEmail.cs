using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EmailSender
{
    public class SendEmail
    {
        string _primatelj;
        string _link;
        string _password;
        public SendEmail(string primatelj, string link, string password)
        {
            _primatelj = primatelj;
            _link = link;
            _password = password;
        }

        public async Task<Response> Execute()
        {

            var apiKey = "";
            //var apiKey = Environment.GetEnvironmentVariable("SENDEMAIL_API");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("info@sm-test.com.hr", "E-Mekteb Aktivacija");
            var subject = "Aktivacija email adrese";
            var to = new EmailAddress(_primatelj, _primatelj);
            var plainTextContent = "Potvrdite svoj račun klikom na link.";
            var htmlContent = _link + "<br><br>Ovo je Vaša lozinka koju savjetujemo da odmah promijenite nakon što se prijavite.<br>Lozinka: " + _password;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return response;


        }
        
    }
}
           


            









                
