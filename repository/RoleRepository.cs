using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RoleRepository : IRoleRepository
{
    private readonly IMongoCollection<Role> _rolesCollection;

    public RoleRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _rolesCollection = database.GetCollection<Role>("Roles");
    }

    public async Task<Role> CreateRoleWithUniqueIntIdAsync(Role newRole)
    {
        List<Role> roles = await GetAllRolesAsync();
        var maxId = 0;
        if (roles.Count > 0)
        {
            maxId = await _rolesCollection.AsQueryable().MaxAsync(user => (int?)user.Id) ?? 0;
        }
        newRole.Id = maxId + 1;
        while (await _rolesCollection.Find(role => role.Id == newRole.Id).AnyAsync())
        {
            newRole.Id++;
        }
        await _rolesCollection.InsertOneAsync(newRole);
        return newRole;
    }

    public async Task<List<Role>> GetAllRolesAsync() =>
        await _rolesCollection.Find(_ => true).ToListAsync();

    public async Task<Role> GetRoleByIdAsync(string id) =>
        await _rolesCollection.Find<Role>(role => role.Id.ToString() == id).FirstOrDefaultAsync();

    public async Task CreateRoleAsync(Role role) =>
        await _rolesCollection.InsertOneAsync(role);

    public async Task UpdateRoleAsync(string id, Role role) =>
        await _rolesCollection.ReplaceOneAsync(role => role.Id.ToString() == id, role);

    public async Task DeleteRoleAsync(string id) =>
        await _rolesCollection.DeleteOneAsync(role => role.Id.ToString() == id);
}