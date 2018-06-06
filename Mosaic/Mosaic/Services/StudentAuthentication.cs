using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mosaic.Models;
using System.Security.Cryptography;
using System.Text;

namespace Mosaic.Services
{
    public class StudentAuthentication : IStudentAuthentication
    {

        private readonly MosaicContext _context;
        public StudentAuthentication (MosaicContext context)
        {
            this._context = context;
        }
        public List<string> ReturnAllUsernames()
        {
            List<string> usernames = new List<string>();
            List<Student> students = _context.Student.ToList();
            foreach (Student s in students)
            {
                usernames.Add(s.Username);
            }
            List<Professor> profs = _context.Professor.ToList();
            foreach (Professor p in profs)
            {
                usernames.Add(p.Username);
            }

            return usernames;
        }

        public Student AllowLogin (string username, string password)
        {

            var student = _context.Student.SingleOrDefault(m => m.Username == username);
            if (student != null) //checking if the username is not already being used
            {
                if (EncryptPassword(password).Equals(student.Password)) //checking if the password corresponds to the same entry as the username in database
                {
                    return student;
                }
            }
            return null;
        }

        public string EncryptPassword (string password)
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

        public Student VerifyChangePassword(string username, string oldPass, string newPass)
        {
            var student = _context.Student.SingleOrDefault(m => m.Username == username);
            if (student.Password.Equals(EncryptPassword(oldPass)))
            {
                student.Password = this.EncryptPassword(newPass);
                return student;
            }
            return null;
        }
    }
}