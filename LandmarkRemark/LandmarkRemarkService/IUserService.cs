using LandmarkRemarkModel;
using System;
using System.Threading.Tasks;

namespace LandmarkRemarkService
{
    public interface IUserService
    {
        Task<string> CreateUser(User user);
        Task<User> GetUser(string userName, string password);
    }
}
