using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using System.Net;

namespace SportStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsStore@example.com";
        public bool UseSsl = true;
        public string UserName = "MySmtpUserName";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        //This option allows us to test this - as its written to file instead of emailed!!!! - this is set from configuration setting using Ninject binding
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings emailSettingsParam)
        {
            _emailSettings = emailSettingsParam;
        }
        
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);

                if(_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation =_emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder();
                body.AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subTotal = line.Product.Price*line.Quantity;
                    body.AppendFormat("{0} x {1} (subTotal: {2:c})", line.Quantity, line.Product.Name, subTotal);
                }

                body.AppendFormat("Total Order Value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line1 ?? String.Empty)
                    .AppendLine(shippingInfo.Line3 ?? String.Empty)
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? String.Empty)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    _emailSettings.MailFromAddress,
                    _emailSettings.MailToAddress,
                    "New Order Submitted!",
                    body.ToString()
                    );

                if(_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);

            }
        }
    }
}
