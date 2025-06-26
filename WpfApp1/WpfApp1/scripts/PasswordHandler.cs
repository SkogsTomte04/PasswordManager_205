using System;
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
        public static string GenerateStrongPassword()
        {
            //Password checklist:
            //needs to be at least 12 ch long
            //compine special characters lowercase and uppercase
            //be unique
            string password = "";

            for(int i = 0; i < passwordLeangth; i++)
            {
                password += GetRandomChar(password);
            }

            return password;
        }
        private static char GetRandomChar(string pass) // Generates a secure password with unique characters, returns one character per call
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
        public static int EstimatePasswordStrength(string password)
{
            if (string.IsNullOrEmpty(password))
            {
                return 0;
            }

            int score = 0;

            if (password.Length >= 8) score++;
            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(ch => !char.IsLetterOrDigit(ch))) score++;

            return score; // goes to 5
}
    }
}
