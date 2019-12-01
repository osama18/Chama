using CoursesDB.Client.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace CoursesDB.Client
{
    public interface IDBClient
    {
        Task<CreateDbResponse> CreateDb(CreateDbRequest request);
        Task<GetDataBasesResponse> GetDataBases();

        Task<GetDataBaseResponse> GetDataBase(GetDataBaseRequest request);
        Task<DeleteDbResponse> DeleteDb(DeleteDbRequest request);

    }
}