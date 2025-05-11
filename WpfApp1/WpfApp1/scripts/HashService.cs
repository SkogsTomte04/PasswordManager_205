using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    class HashService
    {
        public string ComputetoHash(string str)
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

        public bool CheckHash(string newStr, string origin)
        {
            bool check = false;
            if(newStr.Length == origin.Length)
            {
                int i = 0;
                while((i < newStr.Length) && (newStr[i] == origin[i]))
                {
                    i++;
                }
                if(i == newStr.Length)
                {
                    check = true;
                }
            }
            Console.WriteLine("TESTING WRITELINE");
            return check;
        }
    }
}
