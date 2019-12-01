using Chama.Common.Logging;
using Chamma.Common.Settings;
using CoursesDB.Client.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesDB.Client
{
    public class DbClient : IDBClient
    {
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly CosmosClient cosmosClient;

        public DbClient(ISettingProvider settingProvider,ILogger logger)
        {
            this.settingProvider = settingProvider;
            this.logger = logger;
            var connection = settingProvider.GetSetting<string>(Constants.ConnectionString);
            var primaryKey = settingProvider.GetSetting<string>(Constants.PrimaryKey);
            cosmosClient = new CosmosClient(connection,primaryKey);
        }

        public async Task<CreateDbResponse> CreateDb(CreateDbRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");
               
                var result = await cosmosClient.CreateDatabaseAsync(request.Name);
                var database = result.Resource;

                return new CreateDbResponse { 
                    DataBase = database
                };

            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.DataBaseCreationFailed, ex);
                return new CreateDbResponse { 
                    Error = new Error { 
                        Message = "Data Base Creation Failed"
                    }
                };
            }
        }

        public async Task<DeleteDbResponse> DeleteDb(DeleteDbRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException($"request is null");

                var result = await cosmosClient.GetDatabase(request.Name).DeleteAsync();
                var database = result.Resource;

                return new DeleteDbResponse();

            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.DataBasDeleteFailed, ex);
                return new DeleteDbResponse
                {
                    Error = new Error
                    {
                        Message = "Data Base delete Failed"
                    }
                };
            }
        }

        public async Task<GetDataBasesResponse> GetDataBases()
        {
            try
            {
                var iterators = cosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();
                var dataBases = await iterators.ReadNextAsync();
                return new GetDataBasesResponse
                {
                    DataBases = dataBases
                };
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.RetriveDataBasesFailed, ex);
                return new GetDataBasesResponse
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
