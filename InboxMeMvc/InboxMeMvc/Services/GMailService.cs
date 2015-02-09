using System;
using System.Collections.Generic;
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
            var subject = GetSubject(mail);
            client.Send(_config.GmailAccount, mail.EmailTarget, subject, mail.Text);

            return true;
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