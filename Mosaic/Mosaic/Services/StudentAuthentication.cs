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

        private readonly LoginSystemContext _context;
        public StudentAuthentication (LoginSystemContext context)
        {
            this._context = context;
        }

        public int AllowEnroll (string classCode, string username)
        {
                var student = _context.Student.SingleOrDefault(m => m.Username == username.ToUpper());
                if (student != null) //checking if username corresponds to existing student
                {
                    var enrollClass = _context.Class.SingleOrDefault(m => m.ClassCode == classCode.ToUpper());
                    if (enrollClass != null) //checking if classCode corresponds to existing class
                    {
                        string classOne = student.ClassOne;
                        string classTwo = student.ClassTwo;
                        if (classOne == null) //checking if student has an open classOne slot
                        {
                            if (enrollClass.NumEnrolled + 1 <= enrollClass.MaxEnroll) //checking if class is at maximum enrollment
                            {
                                if (!enrollClass.ClassCode.Equals(classOne) && !enrollClass.ClassCode.Equals(classTwo)) //checking if already enrolled in the class
                                {
                                    return 1; //all checks passed, returning 1 to show that student should add class at classOne slot
                                }
                            }
                        } else if (classTwo == null) //checking if student has an open classTwo slot
                        {
                            if (enrollClass.NumEnrolled + 1 <= enrollClass.MaxEnroll) //checking if class is at maximim enrollment
                            {
                                if (!enrollClass.ClassCode.Equals(classOne) && !enrollClass.ClassCode.Equals(classTwo)) //checking if already enrolled in the class
                                {
                                    return 2; //all checks passed, returning 2 to show that student should add class at classTwo slot
                                }
                        }
                        }
                    }
            }
            return 0;
        }

        public int AllowDrop (string classCode, string username)
        {
            var student = _context.Student.SingleOrDefault(m => m.Username == username.ToUpper());
            if (student != null) //checking if username corresponds to existing student
            {
                var dropClass = _context.Class.SingleOrDefault(m => m.ClassCode == classCode.ToUpper());
                if (dropClass != null) //checking if classCode corresponds to existing class
                {
                    string classOne = student.ClassOne;
                    string classTwo = student.ClassTwo;
                    if (classOne != null && classOne.Equals(dropClass.ClassCode)) //checking if student is enrolled in the class in the classOne slot
                    {
                        return 1;
                    }
                    else if (classTwo != null && classTwo.Equals(dropClass.ClassCode)) //checking if student is enrolled in the class in the classTwo slot
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }

        public string AllowCreate (string username, string password, string classOne, string classTwo)
        {
            //different issues with the creating of the student will result in different error codes, allow for more specific error handling
            string errorCode = "";
            var student = _context.Student.SingleOrDefault(m => m.Username == username);
            if (student != null) //checking if username corresponds to existing account
            {
                return "usernametaken"; //error code for username already taken
            } else
            {
                if (username.Length < 5 && password.Length < 5) //checking if username and password satisfy minimum length
                {
                    return "infotooshort";
                }

                //changing the class entries to uppercase to match how they are stored in the database while avoiding null exception
                classOne = (classOne != null) ? classOne.ToUpper() : null;
                classTwo = (classTwo != null) ? classTwo.ToUpper() : null;

                if (classOne != null) //checking if the student is trying to enroll in a class from classOne slot
                {
                    var c1 = _context.Class.SingleOrDefault(m => m.ClassCode == classOne);
                    //checking if the classOne entry corresponds to entry for a class in the database
                    if (c1 == null || c1.NumEnrolled + 1 > c1.MaxEnroll)
                    {
                        errorCode = "c1Invalid";
                    }
                }
                if (classTwo != null)
                {
                    var c2 = _context.Class.SingleOrDefault(m => m.ClassCode == classTwo);
                    //checking if the classTwo entry corresponds to entry for a class in the database
                    if (c2 == null || c2.NumEnrolled + 1 > c2.MaxEnroll)
                    {
                        errorCode += "c2Invalid";
                    }
                }
            }
            return errorCode;
        }

        public bool AllowLogin (string username, string password)
        {

            var student = _context.Student.SingleOrDefault(m => m.Username == username);
            if (student != null) //checking if the username is not already being used
            {
                if (EncryptPassword(password).Equals(student.Password)) //checking if the password corresponds to the same entry as the username in database
                {
                    return true;
                }
            }
            return false;
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
            return null;
        }
    }
}