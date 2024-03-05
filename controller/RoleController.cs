using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RolesController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _roleRepository.GetAllRolesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Role role)
    {   
        await _roleRepository.CreateRoleWithUniqueIntIdAsync(role);
        return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Role role)
    {
        await _roleRepository.UpdateRoleAsync(id, role);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _roleRepository.DeleteRoleAsync(id);
        return NoContent();
    }
}