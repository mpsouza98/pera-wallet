using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace PeraInvest.API.Batch {
    public class DataflowComputations {
        private string filePath = "people-2000000.csv";

        TimeSpan TimeDataflowComputations(int maxDegreeOfParallelism, int messageCount) {
            var workerBlock = new ActionBlock<string>(
               data => SomeWork(),
               new ExecutionDataflowBlockOptions {
                   MaxDegreeOfParallelism = maxDegreeOfParallelism
               });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try {
                using (StreamReader sr = new StreamReader(filePath)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        workerBlock.Post(line);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            workerBlock.Complete();

            // Wait for all messages to propagate through the network.
            workerBlock.Completion.Wait();

            // Stop the timer and return the elapsed number of milliseconds.
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public void execute() {
            int processorCount = Environment.ProcessorCount;
            int messageCount = processorCount;

            // Print the number of processors on this computer.
            Console.WriteLine("Processor count = {0}.", processorCount);

            TimeSpan elapsed;

            // Perform two dataflow computations and print the elapsed
            // time required for each.

            // This call specifies a maximum degree of parallelism of 1.
            // This causes the dataflow block to process messages serially.
            elapsed = TimeDataflowComputations(1, messageCount);
            Console.WriteLine("Degree of parallelism = {0}; message count = {1}; " +
               "elapsed time = {2}ms.", 1, messageCount, (int)elapsed.TotalMilliseconds);

            // Perform the computations again. This time, specify the number of
            // processors as the maximum degree of parallelism. This causes
            // multiple messages to be processed in parallel.
            elapsed = TimeDataflowComputations(processorCount, messageCount);
            Console.WriteLine("Degree of parallelism = {0}; message count = {1}; " +
               "elapsed time = {2}ms.", processorCount, messageCount, (int)elapsed.TotalMilliseconds);
        }

        void SomeWork() {

        }
    }
}
