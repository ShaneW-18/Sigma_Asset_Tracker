using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Sigma3.Util
{
    public class StringUtils
    {
        public static string HashString(string str)
        {
            if (str == null) return String.Empty;

         
           using (var sha256 = new SHA256Managed())
           {
               return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", String.Empty);
           }
            
        }
    }
}
