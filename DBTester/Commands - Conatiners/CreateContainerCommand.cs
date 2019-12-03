using Chama.Dal.Containers.Client;
using Chamma.Common.Settings;
using DBTester.IO;

namespace DBTester
{
    public class CreateContainerCommand : ICreateContainerCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IContainerClient dbClient;

        public CreateContainerCommand(ISettingProvider settings, IWriter writer, IReader reader, IContainerClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Cretae Conatiner"; }
        public string Key { get => "cc"; }

        public void Execute()
        {
            writer.Write("Enter Db Name");
            var dbNmame = reader.ReadMessage();
            writer.Write("Conatiner Id ");
            var conatinerId = reader.ReadMessage();
            writer.Write("Partition Key");
            var partitionKey = reader.ReadMessage();
            writer.Write("ThroughtPut");
            var throughPut = 0;
            bool parsed = int.TryParse(reader.ReadMessage(), out throughPut);
            if(!parsed)
            {
                writer.Write("Invalid number value.");
                return;
            }
            var result = dbClient.CreateContainer(new Chama.Dal.Containers.Client.Model.CreateConatinerRequest{ 
                DbName = dbNmame,
                ConatinerId = conatinerId,
                PartiotionKey = partitionKey,
                Throughput  = throughPut
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
             writer.Write($"Conatiner with ID : {result.ContainerId} Added");
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
