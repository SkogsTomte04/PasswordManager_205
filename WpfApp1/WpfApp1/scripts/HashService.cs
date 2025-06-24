using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    static class HashService
    {
        public static string ComputetoHash(string str) // Converts string to sha256 HASH. TO DO: Add salt
        {
            using(SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
            
        }

        public static bool CheckHash(string newStr, string origin) // compare hash values with eachother, returns true when both are equal.
        {
            bool check = false;
            if(newStr.Length == origin.Length) // check to see if both hash values contain the same amount of characters
            {
                int i = 0;
                while((i < newStr.Length) && (newStr[i] == origin[i])) // compare each character with eachother
                {
                    i++;
                }
                if(i == newStr.Length)
                {
                    check = true;
                }
            }
           
            return check;
        }
    }
}
