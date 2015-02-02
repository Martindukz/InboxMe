using System.Web.Configuration;

namespace InboxMeMvc.Configuration
{
    public class MailGunServiceConfiguration : IMailGunServiceConfiguration
    {
        public string MailAccount {
            get
            {
                var configValue = WebConfigurationManager.AppSettings["gmailaccount"];
                return configValue;   
            }
        }
    }
}