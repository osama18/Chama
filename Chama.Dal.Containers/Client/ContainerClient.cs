using Chama.Common.Logging;
using Chamma.Common.Settings;
using Chama.Dal.Containers.Client.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using CoursesDB.Client;

namespace Chama.Dal.Containers.Client
{
    public class ContainerClient : IContainerClient
    {
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly IDBClient dBClient;
        private const int DefaultThroughput = 400;
        public ContainerClient(ILogger logger, IDBClient dBClient)
        {
            this.dBClient = dBClient;
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

                var result = await dBClient.GetDataBase( new GetDataBaseRequest { Name = request.DbName });

                if (!result.Success)
                    throw new ArgumentException($"No Db found for {request.DbName}");

                var container = await result.Database.CreateContainerAsync(containerDef,request.Throughput ?? DefaultThroughput);
                

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


                var result = await dBClient.GetDataBase(new GetDataBaseRequest { Name = request.DbName });

                if (!result.Success)
                    throw new ArgumentException($"No Db found for {request.DbName}");

                var dataBase = result.Database.GetContainer(request.ConatinerId);


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

                var result = await dBClient.GetDataBase(new GetDataBaseRequest { Name = request.DbName });

                if (!result.Success)
                    throw new ArgumentException($"No Db found for {request.DbName}");

                var iterators = result.Database.GetContainerQueryIterator<ContainerProperties>();

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

        public async Task<GetContainerResponse> GetConatiner(GetContainerRequest request)
        {

            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var result = await dBClient.GetDataBase(new GetDataBaseRequest { Name = request.DbName });

                if (!result.Success)
                    throw new ArgumentException($"No Db found for {request.DbName}");

                var container = result.Database.GetContainer(request.ContainerId);

                return new GetContainerResponse
                {
                    Container = container
                };
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.RetriveDataBasesFailed, ex);
                return new GetContainerResponse
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
