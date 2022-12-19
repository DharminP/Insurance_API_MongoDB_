using CMSAPI.Models;
using MongoDB.Driver;

public class MongoRepository : IMongoRepository
{
    private readonly IConfiguration _configuration;
    public MongoRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoDatabase GetMongoDatabase(string dbName)
    {
        var connString = _configuration.GetConnectionString("Con_MongoDB");
        var client = new MongoClient(connString);
        var db = client.GetDatabase(dbName);
        return db;
    }

    private IMongoCollection<T> GetCollection<T>(string dbName, string collectionName)
    {
        return GetMongoDatabase(dbName).GetCollection<T>(collectionName);
    }

    public async Task AddNewPolicy<policy>(string dbName, string collectionName, Policy _policy)
    {
        var col = GetCollection<policy>(dbName, collectionName);
        var count = col.Find(x => true).CountDocuments();
        _policy.pid = Convert.ToInt32(count) + 1;
        await GetCollection<Policy>(dbName, collectionName).InsertOneAsync(_policy);
    }
    public async Task<List<T>> GetAllPolicy_M<T>(string dbName, string collectionName)
    {
        var col = GetCollection<T>(dbName, collectionName);
        return await col.Find(x => true).ToListAsync();
    }

    public async Task<List<T>> GetFilteredPolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter)
    {
        return await GetCollection<T>(dbName, collectionName).Find(filter).ToListAsync();
    }
    public Boolean DeletePolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter)
    {
        var count = GetCollection<T>(dbName, collectionName).Find(filter).CountDocuments();
        if (count > 0)
        {
            GetCollection<T>(dbName, collectionName).DeleteOneAsync(filter);
            return true;
        }
        return false;
    }

    public async Task UpdatePolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> _policy)
    {
        await GetCollection<T>(dbName, collectionName).UpdateOneAsync(filter, _policy);
    }
}