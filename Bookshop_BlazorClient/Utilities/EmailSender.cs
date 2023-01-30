//using Mailjet.Client;
//using Mailjet.Client.Resources;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Bookshop_BlazorServer.Service;
//using Microsoft.Extensions.Options;
//using Mailjet.Client.TransactionalEmails;

//namespace Bookshop_BlazorClient.Utilities
//{
//    public class EmailSender : IEmailSender
//    {
//        private readonly MailJetSettings _mailJetSetting;

//        public EmailSender(IOptions<MailJetSettings> mailJetSetting)
//        {
//            _mailJetSetting = mailJetSetting.Value;
//        }

//        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
//        {
//           MailjetClient client = new MailjetClient(
//          _mailJetSetting.PublicKey,
//          _mailJetSetting.PrivateKey);

//            MailjetRequest request = new MailjetRequest
//            {
//                Resource = Send.Resource
//            };

//            // construct your email with builder
//            var Email = new TransactionalEmailBuilder()
//                   .WithFrom(new SendContact(_mailJetSetting.Email))
//                   .WithSubject(subject)
//                   .WithHtmlPart(htmlMessage)
//                   .WithTo(new SendContact(email))
//                   .Build();

//            // invoke API to send email
//            var response = await client.SendTransactionalEmailAsync(Email);
//        }
//    }
//}
