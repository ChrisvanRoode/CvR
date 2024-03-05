using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _itemRepository;

    public ItemsController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _itemRepository.GetAllItemsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var item = await _itemRepository.GetItemByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Item item)
    {
        await _itemRepository.CreateItemWithUniqueIntIdAsync(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Item item)
    {
        await _itemRepository.UpdateItemAsync(id, item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _itemRepository.DeleteItemAsync(id);
        return NoContent();
    }
}