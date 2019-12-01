using Chama.Common.Logging;
using Chamma.Common.Settings;
using Chama.Dal.Containers.Client.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace Chama.Dal.Containers.Client
{
    public class ContainerClient : IContainerClient
    {
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly CosmosClient cosmosClient;
        private const int DefaultThroughput = 400;
        public ContainerClient(ISettingProvider settingProvider,ILogger logger)
        {
            this.settingProvider = settingProvider;
            this.logger = logger;
            var connection = settingProvider.GetSetting<string>(Constants.ConnectionString);
            var primaryKey = settingProvider.GetSetting<string>(Constants.PrimaryKey);
            cosmosClient = new CosmosClient(connection,primaryKey);
        }


        public async Task<CreateContainerResponse> CreateContainer(CreateConatinerRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                if (string.IsNullOrWhiteSpace(request.ConatinerId))
                    throw new ArgumentNullException($"request has null conatiner id");

                var containerDef = new ContainerProperties
                {
                    Id = request.ConatinerId,
                    PartitionKeyPath = request.PartiotionKey ?? "/partitionKey"
                };

                var dataBase = cosmosClient.GetDatabase(request.DbName);

                if (dataBase == null)
                    throw new InvalidOperationException($"No Db found with name {request.DbName}");

                var result = await dataBase.CreateContainerAsync(containerDef,request.Throughput ?? DefaultThroughput);
                

                return new CreateContainerResponse
                {
                    ContainerId = containerDef.Id
                };

            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.DataBaseCreationFailed, ex);
                return new CreateContainerResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base Creation Failed"
                    }
                };
            }
        }

        public async Task<DeleteContainerResponse> DeleteConatiner(DeleteContainerRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var dataBase = cosmosClient.GetContainer(request.DbName,request.ConatinerId);

                if (dataBase == null)
                    throw new InvalidOperationException($"No Db found with name {request.DbName}");

                await dataBase.DeleteContainerAsync();

                return new DeleteContainerResponse ();

            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.DataBasDeleteFailed, ex);
                return new DeleteContainerResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base delete Failed"
                    }
                };
            }
        }


        public async Task<GetContainersResponse> GetConatiners(GetContainersRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var dataBase = cosmosClient.GetDatabase(request.Name);

                if (dataBase == null)
                    throw new InvalidOperationException($"No Db found with name {request.Name}");

                var iterators = dataBase.GetContainerQueryIterator<ContainerProperties>();
                var containers = await iterators.ReadNextAsync();
                return new GetContainersResponse
                {
                    Containers = containers
                };
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.RetriveDataBasesFailed, ex);
                return new GetContainersResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base retrieve Failed"
                    }
                };
            }
        }

    }
}
