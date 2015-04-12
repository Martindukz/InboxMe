using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using InboxMeMvc.Models;
using InboxMeMvc.Services;

namespace InboxMeMvc.Controllers
{
    public class SimpleTextMailController : ApiController
    {
        private readonly IMailService _mailService;

        public SimpleTextMailController()
            : this(null)
        {
        }

        public SimpleTextMailController(IMailService mailService)
        {
            //_mailService = mailService ?? new MailGunService();
            _mailService = mailService ?? new GMailService();
        }

        public SimpleTextMail Get()
        {
            var configValue = WebConfigurationManager.AppSettings["testofappharbour"];
            return new SimpleTextMail()
                {
                    EmailTarget = "sometarget@somewhere.com",
                    Text = "Some text",
                    Subject = "Somesubject",
                    Token = configValue,
                };
        }

        public HttpResponseMessage Post(SimpleTextMail mail)
        {
            //Convert to some general mail format, e.g. text and attachments (pictures, video, audio)

            var omnitoken = WebConfigurationManager.AppSettings["omnitoken"];
            if (mail == null)
            {
                //throw new NullReferenceException();
                mail = CreateNullMailResponse(mail);
            }
            else if (mail.Token != omnitoken)
            {
                //throw new SecurityException("Invalid token");
                mail = CreateFaultyTokenResponse(mail, omnitoken);
            }
            else
            {
                try
                {
                    var fileCollection = HttpContext.Current.Request.Files; 
                    _mailService.SendSimpleMail(mail);
                }
                catch (Exception ex)
                {
                    mail = CreateExceptionResponse(ex);
                }
            }
            var response = Request.CreateResponse<SimpleTextMail>(System.Net.HttpStatusCode.Created, mail);

            return response;
        }

        private static SimpleTextMail CreateExceptionResponse(Exception ex)
        {
            SimpleTextMail mail;
            mail = new SimpleTextMail()
                {
                    EmailTarget = "NotSent",
                    Text = "Failed with exception: " + ex,
                    Token = "Ex message: " + ex.Message
                };
            return mail;
        }

        private static SimpleTextMail CreateFaultyTokenResponse(SimpleTextMail mail, string omnitoken)
        {
            mail = new SimpleTextMail()
                {
                    EmailTarget = "NotSent",
                    Text = "Wrong token. Actual token startswith: " + omnitoken.Substring(0, 5),
                    Token = mail.Token
                };
            return mail;
        }

        private static SimpleTextMail CreateNullMailResponse(SimpleTextMail mail)
        {
            mail = new SimpleTextMail()
                {
                    EmailTarget = "NotSent",
                    Text = "Mail was null - possible caused by bad format",
                    Token = mail.Token
                };
            return mail;
        }
    }
}
