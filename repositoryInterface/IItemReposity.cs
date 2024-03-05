using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemRepository
{
    Task<List<Item>> GetAllItemsAsync();
    Task<Item> GetItemByIdAsync(string id);
    Task<Item> CreateItemWithUniqueIntIdAsync(Item item);
    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(string id, Item item);
    Task DeleteItemAsync(string id);
}