using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(string id);
    Task<Role> CreateRoleWithUniqueIntIdAsync(Role newRole);
    Task CreateRoleAsync(Role role);
    Task UpdateRoleAsync(string id, Role role);
    Task DeleteRoleAsync(string id);
}