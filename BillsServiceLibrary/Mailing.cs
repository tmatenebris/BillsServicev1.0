using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace BillsServiceLibrary
{
    public static class Mailing
    {

        //private static SmtpClient _client= new SmtpClient("smtp.gmail.com", 587)
        //{
        //    Credentials = new NetworkCredential("infobillservice@gmail.com", "irdldjtolbtcshgg"),
        //    EnableSsl = true
        //};

       
           private static SmtpClient _client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"], int.Parse(ConfigurationManager.AppSettings["SMTPPort"]))
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["GMailAccount"], ConfigurationManager.AppSettings["GMailAppKey"]),
                EnableSsl = true
            };
            private static MailAddress _address = new MailAddress(ConfigurationManager.AppSettings["GMailAccount"]);
        
        

        //private static MailAddress _address = new MailAddress(ConfigurationManager.AppSettings["GMailAccount"]);
        public static void SendMail(string mail, string topic, string userData, string attachPath)
        {
           // _client = new SmtpClient("smtp.gmail.com", 587)
            //{
             // Credentials = new NetworkCredential("infobillservice@gmail.com", "irdldjtolbtcshgg"),
             // EnableSsl = true
            //};
            //_address = new MailAddress("infobillservice@gmail.com");
            MailMessage message = new MailMessage();
            message.From = _address;
            message.To.Add(mail);
            message.Subject = topic;
            message.Body = "Hi " + userData + "!\nYour new bill arrived!\nData in the attachment!\nBest regards,\nBilly - The BillService";
            Attachment attachment = new Attachment(attachPath);
            message.Attachments.Add(attachment);

            _client.Send(message);

        }

        public static void SendError(string product, string mail, string userData)
        {
            //_client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("infobillservice@gmail.com", "irdldjtolbtcshgg"),
            //    EnableSsl = true
            //};
            //_address = new MailAddress("infobillservice@gmail.com");
            MailMessage message = new MailMessage();
            message.From = _address;
            message.To.Add(mail);
            message.Subject = "Error, product: " + product;
            message.Body = "Hi " + userData + "!\nYour latest bill is not valid!\nBilly - The BillService";
            _client.Send(message);

        }
    }
}
