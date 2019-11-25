using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACME_WebApp.Models
{
    public class UserSingleton
    {
        UserSingleton()
        {
        }
        private static readonly object padlock = new object();
        private static UserSingleton instance = null;
        public static User user { get; set; }
        public static UserSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserSingleton();
                    }
                    return instance;
                }
            }
        }
    }
}