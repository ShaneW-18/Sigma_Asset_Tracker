using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma3.Objects
{
    internal class User
    {
        private String password { get; set; }
        private String email { get; set; }
        private String phone { get; set; }
        private readonly Guid ID = new Guid();
        private Notifications notifications = new Notifications();
        private bool porfolioCreated = false;

        public User(String password, String email, String phone)
        {
            this.password = password;
            this.email = email;
            this.phone = phone;
        }
        public bool createPorfolio()
        {
            if (porfolioCreated)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
       
        public void addNotification()
        {
            
        }
        
        public Guid getID()
        {
            return ID;  
        }



        
        

        

    }
}
