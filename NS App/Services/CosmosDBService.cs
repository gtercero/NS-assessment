using Microsoft.Azure.Cosmos;
using NS_App.Entities;
using NS_App.Models;
using NS_App.Services.Interfaces;

namespace NS_App.Services
{
    public class CosmosDbService : ICosmosDBService
    {
        private Container _container;
        public CosmosDbService(CosmosClient cosmosDbClient, string databaseName, string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<FileViewModel>> GetAsync()
        {
            var query = _container.GetItemQueryIterator<FileViewModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<FileViewModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task AddAsync(Metadata Metadata)
        {
            await _container.UpsertItemAsync(Metadata, new PartitionKey(Metadata.Id));
        }


        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Metadata>(id, new PartitionKey(id));
        }


    }
}
