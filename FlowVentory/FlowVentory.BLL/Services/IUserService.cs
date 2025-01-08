using Flowventory.DL.Models;

namespace FlowVentory.BLL.Services;

public interface IUserService
{
    User GetUserByUsername(string username);
    bool VerifyPassword(string password, string hashedPassword);
    string HashPassword(string password);
    void CreateUser(User user);
}