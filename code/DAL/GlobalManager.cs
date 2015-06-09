using System;
using System.Collections.Generic;

using Talker.ML;

namespace Talker.DAL
{
    public class GlobalManager
    {
        private static GlobalManager mInstance = new GlobalManager();

        private GlobalManager()
        {
            Friends = new List<User>();
            CurrentMessages = new List<Message>();
        }

        public static GlobalManager Instance
        {
            get 
            { 
                return mInstance;
            }
        }

		public void Init(IUserDBLocal pUserServiceLocal, IUserDBRemote pUserServiceRemote, IMessageDB pMessageService)
        {
            UserServiceLocal = pUserServiceLocal;
            MessageseService = pMessageService;
			UserServiceRemote = pUserServiceRemote;
        }

        // Services
        public IUserDBLocal UserServiceLocal { get; private set; }
        public IMessageDB MessageseService { get; private set; }
		public IUserDBRemote UserServiceRemote { get; private set; }

        // Data
        public User CurrentUser { get; set; }
        public List<User> Friends { get; set; }
        public List<Message> CurrentMessages { get; set; }
    }
}

