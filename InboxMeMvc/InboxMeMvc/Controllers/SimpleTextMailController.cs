using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Configuration;
using System.Web.Http;
using InboxMeMvc.Models;
using InboxMeMvc.Services;

namespace InboxMeMvc.Controllers
{
    public class SimpleTextMailController : ApiController
    {
        private readonly IMailService _mailService;

        public SimpleTextMailController() : this(null)
        {
        }

        public SimpleTextMailController(IMailService mailService)
        {
            _mailService = mailService ?? new GMailService(); 
        }

        public SimpleTextMail Get()
        {
            var configValue = WebConfigurationManager.AppSettings["testofappharbour"];
            return new SimpleTextMail()
                {
                    EmailTarget = "sometarget@somewhere.com",
                    Text = "Some text",
                    Token = configValue
                };
        }

        public HttpResponseMessage Post(SimpleTextMail mail)
        {
            //Convert to some general mail format, e.g. text and attachments (pictures, video, audio)
            
            var omnitoken = WebConfigurationManager.AppSettings["omnitoken"];
            if (mail.Token != omnitoken)
            {
                throw new SecurityException("Invalid token");
            }

            _mailService.SendSimpleMail(mail);

            var response = Request.CreateResponse<SimpleTextMail>(System.Net.HttpStatusCode.Created, mail);

            return response;
        }
    }
}
