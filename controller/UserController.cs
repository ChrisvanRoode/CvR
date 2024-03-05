using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly UserService _userService;

    public UsersController(IUserRepository userRepository, IRoleRepository roleRepository, UserService userService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userRepository.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user, [FromQuery] int roleId)
    {   
        user.Role = await _roleRepository.GetRoleByIdAsync(roleId.ToString());
        _userService.EmailAboutCreatedUser(user);
        await _userRepository.CreateUserWithUniqueIntIdAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] User user)
    {
        await _userRepository.UpdateUserAsync(id, user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _userRepository.DeleteUserAsync(id);
        return NoContent();
    }
}