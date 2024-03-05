using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ItemRepository : IItemRepository
{
    private readonly IMongoCollection<Item> _itemsCollection;

    public ItemRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _itemsCollection = database.GetCollection<Item>("Items");
    }
    
    public async Task<Item> CreateItemWithUniqueIntIdAsync(Item newItem)
    {
        var maxId = await _itemsCollection.AsQueryable().MaxAsync(item => (int?)item.Id) ?? 0;
        newItem.Id = maxId + 1;
        while (await _itemsCollection.Find(item => item.Id == newItem.Id).AnyAsync())
        {
            newItem.Id++;
        }
        await _itemsCollection.InsertOneAsync(newItem);
        return newItem;
    }

    public async Task<List<Item>> GetAllItemsAsync() =>
        await _itemsCollection.Find(_ => true).ToListAsync();

    public async Task<Item> GetItemByIdAsync(string id) =>
        await _itemsCollection.Find<Item>(item => item._id.ToString() == id).FirstOrDefaultAsync();

    public async Task CreateItemAsync(Item item) =>
        await _itemsCollection.InsertOneAsync(item);

    public async Task UpdateItemAsync(string id, Item item) =>
        await _itemsCollection.ReplaceOneAsync(item => item._id.ToString() == id, item);

    public async Task DeleteItemAsync(string id) =>
        await _itemsCollection.DeleteOneAsync(item => item._id.ToString() == id);
}