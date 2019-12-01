using Chama.Dal.Containers.Client.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Chama.Dal.Containers.Client
{
    public interface IContainerClient
    {
        Task<CreateContainerResponse> CreateContainer(CreateConatinerRequest request);
        Task<GetContainersResponse> GetConatiners(GetContainersRequest request);

        Task<GetContainerResponse> GetConatiner(GetContainerRequest request);
        Task<DeleteContainerResponse> DeleteConatiner(DeleteContainerRequest request);

    }
}