using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<User> CreateUserWithUniqueIntIdAsync(User newUser);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(string id, User user);
    Task DeleteUserAsync(string id);
}