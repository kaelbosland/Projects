using Mosaic.Models;

namespace Mosaic.Services
{
    public interface IStudentAuthentication
    {
        int AllowEnroll (string classCode, string username);
        int AllowDrop (string classCode, string username);
        bool AllowLogin(string username, string password);
        string AllowCreate(string username, string password, string classOne, string classTwo);
        string EncryptPassword(string password);
        Student VerifyChangePassword(string username, string oldPass, string newPass);
    }
}
