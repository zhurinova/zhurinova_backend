using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace zhurinova_backend.Model
{
    public class User
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool IsAdmin => Login == "admin";
    }
}
