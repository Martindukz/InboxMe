using InboxMeMvc.Models;

namespace InboxMeMvc.Services
{
    public interface IMailService
    {
        bool SendSimpleMail(SimpleTextMail mail); 
    }
}