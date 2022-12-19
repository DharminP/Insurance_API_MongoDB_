using CMSAPI.Models;
using MongoDB.Driver;

public interface IMongoRepository
{
    Task<List<T>> GetAllPolicy_M<T>(string dbName, string collectionName);
    Task AddNewPolicy<T>(string dbName, string collectionName, Policy _policy);
    Task<List<T>> GetFilteredPolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter);
    Boolean DeletePolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter);
    Task UpdatePolicy<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> _policy);

}