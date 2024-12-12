using Flowventory.DL.Models;
using System.Security.Cryptography;
using Flowventory.DL.Data;

public class AuthenticationService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Login(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user != null && VerifyPassword(password, user.Password))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Register(string username, string password, string email)
    {
        var existingUser   = _dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (existingUser   != null)
        {
            return false;
        }
        else
        {
            var hashedPassword = HashPassword(password);
            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        // Simple password verification
        var bytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[16];
        Array.Copy(bytes, 0, salt, 0, 16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        var key = pbkdf2.GetBytes(20);
        var storedKey = new byte[20];
        Array.Copy(bytes, 16, storedKey, 0, 20);
        return key.SequenceEqual(storedKey);
    }

    private string HashPassword(string password)
    {
        // Simple password hashing
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        var key = pbkdf2.GetBytes(20);
        var bytes = new byte[36];
        Array.Copy(salt, 0, bytes, 0, 16);
        Array.Copy(key, 0, bytes, 16, 20);
        return Convert.ToBase64String(bytes);
    }
    
    public User GetUserByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Username == username);
    }
}