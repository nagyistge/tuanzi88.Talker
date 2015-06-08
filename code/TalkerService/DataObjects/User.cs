using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkerService.DataObjects
{
    public class User : EntityData
    {
        public string mName { get; set; }
        public byte[] mSalt { get; set; }
        public byte[] mSaltedAndHashedPd { get; set; }

        public string mUserType { get; set; }

        [InverseProperty("mSender")]
        public virtual ICollection<Message> mSentMessages { get; set; }

        [InverseProperty("mReceiver")]
        public virtual ICollection<Message> mReceivedMessages { get; set; }
    }
}
