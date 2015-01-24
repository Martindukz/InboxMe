using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InboxMeMvc.Models;

namespace InboxMeMvc.Services
{
    public class MailService : IMailService
    {
        public bool SendSimpleMail(SimpleTextMail mail)
        {
            //todo. 
            return true; 
        }
    }
}