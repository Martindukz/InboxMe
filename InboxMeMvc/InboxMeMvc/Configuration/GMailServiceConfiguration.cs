using System.Web.Configuration;

namespace InboxMeMvc.Configuration
{
    public class GMailServiceConfiguration : IGMailServiceConfiguration
    {
        public string GmailAccount {
            get
            {
                var configValue = WebConfigurationManager.AppSettings["gmailaccount"];
                return configValue;
            }
        }
        public string GmailAccountPassword {
            get
            {
                var configValue = WebConfigurationManager.AppSettings["gmailaccountpassword"];
                return configValue;
                
            }
        }
    }
}