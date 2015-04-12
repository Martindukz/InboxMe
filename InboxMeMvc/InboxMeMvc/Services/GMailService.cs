using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using InboxMeMvc.Configuration;
using InboxMeMvc.Models;

namespace InboxMeMvc.Services
{
    public class GMailService : IMailService
    {
        private readonly IGMailServiceConfiguration _config;
        private const int _defaultSubjectLength = 25;

        public GMailService() : this(null)
        {
        }

        public GMailService(IGMailServiceConfiguration config)
        {
            _config = config ?? new GMailServiceConfiguration();
        }

        public bool SendSimpleMail(SimpleTextMail mail)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_config.GmailAccount, _config.GmailAccountPassword),
                EnableSsl = true
            };
            using (var mailMessage = CreateMailMessage(mail))
            {
                client.Send(mailMessage);
            }
            
            return true;
        }

        private MailMessage CreateMailMessage(SimpleTextMail mail)
        {
            var subject = GetSubject(mail);
            var result = new MailMessage(_config.GmailAccount, mail.EmailTarget, subject, mail.Text);

            HandleAttachments(result, mail);

            return result;
        }

        private void HandleAttachments(MailMessage result, SimpleTextMail mail)
        {
            if (string.IsNullOrWhiteSpace(mail.FileString))
            {
                return;
            }
            var fileBytes = Convert.FromBase64String(mail.FileString);
            var memStream = new MemoryStream(fileBytes);

            string fileName = mail.FileName ?? "unknownfile.dat";
            var attachment = new Attachment(memStream, fileName);
            result.Attachments.Add(attachment);
        }

        private string GetSubject(SimpleTextMail mail)
        {
            if (string.IsNullOrWhiteSpace(mail.Text))
                return "";

            return mail.Subject;

            //var subjectLength = Math.Min(_defaultSubjectLength, mail.Text.Length);

            //return mail.Text.Substring(0, subjectLength);
        }
    }
}