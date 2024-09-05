using System.Threading.Tasks.Dataflow;

namespace PeraInvest.API.Batch {

    public class DataflowProducerConsumer {
        public DataflowProducerConsumer() { }
        static void Produce(ITargetBlock<string> target) {
            string filePath = "people-2000000.csv";
            var rand = new Random();

            try {
                using (StreamReader sr = new StreamReader(filePath)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        target.SendAsync(line);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            target.Complete();
        }

        static async Task ConsumeAsync(IReceivableSourceBlock<string> source, int consumerId) {
            while (await source.OutputAvailableAsync()) {
                string data = await source.ReceiveAsync();
                // Do some processing
            }
        }

        public async Task Main() {
            var buffer = new BufferBlock<string>();
            var consumerTask = ConsumeAsync(buffer, 1);
            Produce(buffer);

            await consumerTask;
        }
    }

}
