using iText.Kernel.Pdf;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using WebApi.Models.DB;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Net;
using System;
using System.Web.Hosting;
using System.Linq;

namespace WebApi.CustomHelp
{
    public static class CustomHelper
    {
        private static FAGSYSTEMDBEntities db = new FAGSYSTEMDBEntities();

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private static byte[] DataFile()
        {
            byte[] pdfBytes = null;

            using (var mstream = new MemoryStream())
            {
                using (var writer = new PdfWriter(mstream))
                {
                    using (var userDataPdf = new PdfDocument(writer))
                    {
                        //Generate Text for PDF bilforsikring avtale
                    }
                }
                if (mstream != null)
                {
                    pdfBytes = mstream.ToArray();
                    return pdfBytes;
                }
            }
            return null;
        }

        private static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo, string role)
        {
            string from, to, bcc, cc, subject, body;
            from = "";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            var sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();

            // String CONFIGSET = "ConfigSet";
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(from),
            };

            // mail.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            if (!string.IsNullOrEmpty(role))
            {
                string filename = "bilforsikringavtale.pdf";
                byte[] bytes = DataFile();
                var data = new Attachment(new MemoryStream(bytes), filename);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(filename);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(filename);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(filename);
                if (data != null)
                {
                    mail.Attachments.Add(data);
                }
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        private static void SendEmail(MailMessage mail)
        {
            try
            {
                //Dette kan også legges i webConfig 
                var credential = new NetworkCredential
                {
                    UserName = "",
                    Password = ""
                };

                var client = new SmtpClient
                {
                    Host = "",
                    Port = 0,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    Credentials = credential,
                    EnableSsl = true,
                };

                // Console.WriteLine("Attempting to send bilforskiring post...");
                client.Send(mail);
                // Console.WriteLine("Bilforsikring post er sent!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void BuildEmailTemplate(Kunder user)
        {
            var verifybody = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EpostTemplate/") + "AvtaleText" + ".cshtml");
            if (user != null)
            {
                var aktivUser = db.Kunders.Where(x => x.Id == user.Id).FirstOrDefault();
                BuildEmailTemplate("Bilforsikring avtale har blitt sendt!", verifybody, aktivUser.Email, "User");
            }
        }
        public static double BeregnPrice()
        {
            var forsikringPris = 0.0;
            try {
                return forsikringPris;
            }
            catch 
            (Exception)
            {
                throw;
            }
        }
    }
}