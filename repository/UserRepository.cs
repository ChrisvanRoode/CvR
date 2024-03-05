using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>("Users");
    }

    public async Task<User> CreateUserWithUniqueIntIdAsync(User newUser)
    {
        var maxId = await _usersCollection.AsQueryable().MaxAsync(user => (int?)user.Id) ?? 0;
        newUser.Id = maxId + 1;
        while (await _usersCollection.Find(user => user.Id == newUser.Id).AnyAsync())
        {
            newUser.Id++;
        }
        await _usersCollection.InsertOneAsync(newUser);
        return newUser;
    }

    public async Task<List<User>> GetAllUsersAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User> GetUserByIdAsync(string id) =>
        await _usersCollection.Find<User>(user => user._id.ToString() == id).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User user) =>
        await _usersCollection.InsertOneAsync(user);

    public async Task UpdateUserAsync(string id, User user) =>
        await _usersCollection.ReplaceOneAsync(user => user._id.ToString() == id, user);

    public async Task DeleteUserAsync(string id) =>
        await _usersCollection.DeleteOneAsync(user => user._id.ToString() == id);
}