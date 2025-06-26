using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    
    public class Credential
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string Email { get; set; }
        public string ServiceName { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
