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

        public void Init(IUserService pUserService, IMessageService pMessageService)
        {
            UserService = pUserService;
            MessageseService = pMessageService;
        }

        // Services
        public IUserService UserService { get; private set; }
        public IMessageService MessageseService { get; private set; }

        // Data
        public User CurrentUser { get; set; }
        public List<User> Friends { get; set; }
        public List<Message> CurrentMessages { get; set; }
    }
}

