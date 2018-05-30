using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mosaic.Models;
using System.Security.Cryptography;
using System.Text;

namespace Mosaic.Services
{
    public class ProfAuthentication : IProfAuthentication
    {
        private readonly LoginSystemContext _context;

        public ProfAuthentication (LoginSystemContext context)
        {
            _context = context;
        }

        public bool AllowLogin(string username, string password)
        {
            var prof = _context.Professor.SingleOrDefault(m => m.Username == username);
            if (prof != null)
            {
                if (prof.Password.Equals(EncryptPassword(password)))
                {
                    return true;
                }
            }

            return true;
        }

        public string EncryptPassword(string password)
        {
            string encrypted = "";
            using (SHA512 crypto = new SHA512Managed())
            {
                byte[] passwordInBytes = Encoding.ASCII.GetBytes(password);
                byte[] hash = crypto.ComputeHash(passwordInBytes);
                encrypted = Encoding.ASCII.GetString(hash);
            }
            return encrypted;
        }
        public Professor VerifyChangePassword(string username, string oldPass, string newPass)
        {
            var prof = _context.Professor.SingleOrDefault(m => m.Username == username);
            if (prof.Password.Equals(EncryptPassword(oldPass)))
            {
                prof.Password = this.EncryptPassword(newPass);
                return prof;
            }
            return null;
        }
    }
}
