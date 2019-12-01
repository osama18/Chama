using Chama.Common.Logging;
using Chama.Dal.Containers.Client;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.OData.Formatter;

namespace GenerIcRepository
{
    internal class GenericRepository<T> where T: EtagEntity
    {
        private readonly ILogger logger;
        private readonly IContainerClient containerClient;
        
        public GenericRepository(IContainerClient containerClient, ILogger logger)
        {
            this.containerClient = containerClient;
            this.logger = logger;
        }


        public async Task<string> CreateDocument(string dbName, string containerId, T document)
        {
            document.Id = Guid.NewGuid().ToString();

            await (await GetContainerAsync(dbName, containerId)).CreateItemAsync<T>(document);

            return document.Id;
        }

        public async Task<T> Retrieve(string dbName, string containerId, string id)
        {
            var result =  (await GetContainerAsync(dbName, containerId))
                .GetItemLinqQueryable<T>(false)
                .Where(s => s.Id == id).SingleOrDefault();
            
            return result;
        }

        public async Task<T> Retrieve(string dbName, string containerId, Guid id, PartitionKey partitionKey)
        {
            var container = await GetContainerAsync(dbName, containerId);

            var result = await container.ReadItemAsync<T>(
             partitionKey: partitionKey,
             id: id.ToString());

            return result;
        }

        
        public async Task<T> UpdateDocument(string dbName, string containerId, T document, Guid id)
        {
            await (await GetContainerAsync(dbName, containerId)).ReplaceItemAsync<T>(document,id.ToString());

            return document;
        }

        public async Task<T> UpdateDocumentOptimisticConcurrency(string dbName,
          string containerId,
          T document,
          PartitionKey partitionKey)
        {
            var container = await GetContainerAsync(dbName, containerId);

            try
            {
                await container.ReplaceItemAsync<T>(document, document.Id.ToString(), partitionKey, new ItemRequestOptions { IfMatchEtag = document.ETag});
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                    await logger.LogException(LogEvent.ConcurrencyIssue, ex );
                }
                throw ex;
            }
            
            return document;
        }

        public async Task DeleteDocument(string dbName, string containerId, Guid id, PartitionKey partiotionKey)
        {
            await (await GetContainerAsync(dbName, containerId)).DeleteItemAsync<T>(id.ToString(), partiotionKey);
        }

        protected async Task<Container> GetContainerAsync(string dbName, string containerId)
        {
            var response = await containerClient.GetConatiner(new GetContainerRequest
            {
                ContainerId = containerId,
                DbName = dbName
            });

            return response.Container;
        }
    }
}
