using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Sigma3.Objects
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public decimal PortfolioBalance { get; set; }

        
    }
}
