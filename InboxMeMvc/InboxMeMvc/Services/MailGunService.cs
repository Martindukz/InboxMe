using System;
using System.Net.Mail;
using InboxMeMvc.Configuration;
using InboxMeMvc.Models;

namespace InboxMeMvc.Services
{
    public class MailGunService : IMailService
    {
        private readonly IGMailServiceConfiguration _config;
        private const int _defaultSubjectLength = 25;

        public MailGunService()
            : this(null)
        {
        }

        public MailGunService(IGMailServiceConfiguration config)
        {
            _config = config ?? new GMailServiceConfiguration();
        }

        public bool SendSimpleMail(SimpleTextMail mail)
        {
            var client = new SmtpClient();
            var subject = GetSubject(mail);
            client.Send(_config.GmailAccount, mail.EmailTarget, subject, mail.Text);

            return true;
        }

        private string GetSubject(SimpleTextMail mail)
        {
            if (string.IsNullOrWhiteSpace(mail.Text))
                return "";

            var subjectLength = Math.Min(_defaultSubjectLength, mail.Text.Length);

            return mail.Text.Substring(0, subjectLength);
        }
    }
}