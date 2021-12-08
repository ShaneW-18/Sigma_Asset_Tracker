using System;
using System.Collections.Generic;
using System.Text;
using Sigma3.Objects;

namespace Sigma3
{
    public class Constans
    {
        public static readonly User DEMO_USER = new User
        {
            Name = "John Doe",
            PhoneNumber = "000-000-0000",
            Email = "Demo",
            Password = "Demo",
            PortfolioBalance = 0
        };

        public static readonly bool DEMO_ENABLED = true;
    }
}
