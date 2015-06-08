using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Mobile.Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkerService.DataObjects
{
    public class Message : EntityData
    {
        public string mSenderID { get; set; }
        public string mReceiverID { get; set; }
        public string mText { get; set; }
        public bool mHasRead { get; set; }

        [ForeignKey("mSenderID")]
        public virtual User mSender { get; set; }

        [ForeignKey("mReceiverID")]
        public virtual User mReceiver { get; set; }
    }
}
