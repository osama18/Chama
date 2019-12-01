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
    internal class GenericRepository<T> : IGenericRepository<T> where T: EtagEntity
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

        public async Task<T> Retrieve(string dbName, string containerId, string id, string partitionKey)
        {
            
            var container = await GetContainerAsync(dbName, containerId);

            var result = await container.ReadItemAsync<T>(
             partitionKey: new PartitionKey(partitionKey),
             id: id);

            return result;
        }

        
        public async Task<T> UpdateDocument(string dbName, string containerId, T document, string id)
        {
            await (await GetContainerAsync(dbName, containerId)).ReplaceItemAsync<T>(document,id);

            return document;
        }

        public async Task<T> UpdateDocumentOptimisticConcurrency(string dbName,
          string containerId,
          T document,
          string partitionKey)
        {
            var container = await GetContainerAsync(dbName, containerId);

            try
            {
                await container.ReplaceItemAsync<T>(document, document.Id, new PartitionKey(partitionKey), new ItemRequestOptions { IfMatchEtag = document.ETag});
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

        public async Task DeleteDocument(string dbName, string containerId, string id, string partiotionKey)
        {
            await (await GetContainerAsync(dbName, containerId)).DeleteItemAsync<T>(id, new PartitionKey(partiotionKey));
        }

        protected async Task<Container> GetContainerAsync(string dbName, string containerId)
        {
            var response = await containerClient.GetConatiner(new GetContainerRequest
            {
                ContainerId = containerId,
                DbName = dbName
            });

            if (!response.Success)
                throw new ArgumentException($"No container found for {containerId} in Db : {dbName}");

            return response.Container;
        }
    }
}
