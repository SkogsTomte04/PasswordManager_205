﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    class PasswordHandler
    {
        private const string letters = "qwertyuiopasdfghjklzxcvbnm";
        private const string upperLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        private const string numbers = "0123456789";
        private const string characters = """!@#$%^&*()_-+={}[|]:;\"'<>.,/?~""";
        private const int passwordLeangth = 12;
        public string GenerateStrongPassword()
        {
            //Password checklist:
            //needs to be at least 12 ch long
            //compine special characters lowercase and uppercase
            //be unique
            string password = "";

            for(int i = 0; i < 12; i++)
            {
                password += GetRandomChar(password);
            }

            return password;
        }
        private char GetRandomChar(string pass) // Generates a secure password with unique characters, returns one character per call
        {
            char ch = ' ';
            Random rnd = new Random();
            string[] strings = [letters, upperLetters, numbers, characters];

            bool uniqueChar = false;

            while (!uniqueChar)
            {
                int chartype = rnd.Next(strings.Length);
                int charPos = rnd.Next(strings[chartype].Length);
                ch = strings[chartype][charPos];

                if (!pass.Contains(ch))
                {
                    uniqueChar = true;
                }

            }
            
            return ch;
        }
    }
}
