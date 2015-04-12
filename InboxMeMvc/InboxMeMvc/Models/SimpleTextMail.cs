using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace InboxMeMvc.Models
{
    [DataContract]
    public class SimpleTextMail
    {
        
        //TODO: Move to base class: 

        [DataMember]
        public string EmailTarget { get; set; }
        //TODO: Move to base class: 
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string FileString { get; set; }
        [DataMember]
        public string FileName { get; set; }
    }
}