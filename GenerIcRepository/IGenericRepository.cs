using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace GenerIcRepository
{
    public interface IGenericRepository<T> where T : EtagEntity
    {

        Task<string> CreateDocument(string dbName, string containerId, T document);

        Task<T> Retrieve(string dbName, string containerId, string id);

        Task<T> Retrieve(string dbName, string containerId, string id, string partitionKey);


        Task<T> UpdateDocument(string dbName, string containerId, T document, string id);
        Task<T> UpdateDocumentOptimisticConcurrency(string dbName,
          string containerId,
          T document,
          string partitionKey);

        Task DeleteDocument(string dbName, string containerId, string id, string partiotionKey);


    }
}