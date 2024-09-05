using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeraInvest.API.Commands.Handlers;
using PeraInvest.API.Commands;
using System.Threading.Tasks.Dataflow;
using PeraInvest.API.Batch;
using System.Diagnostics;

namespace PeraInvest.API.Controllers {

    [Route("/api/batch")]
    [ApiController]
    public class BatchController : ControllerBase {
        private readonly ILogger<AtivoFinanceiroController> logger;

        public BatchController(ILogger<AtivoFinanceiroController> logger) {
            this.logger = logger;
        }

        [HttpPost]
        [Route("/producerconsumer")]
        public async Task<ActionResult> PostDataFlowProducerConsumer() {
            DataflowProducerConsumer dataflowBlock = new DataflowProducerConsumer();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            await dataflowBlock.Main();
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

            return Accepted(nameof(PostDataFlowProducerConsumer));
        }

        [HttpPost]
        [Route("/dflowcomp")]
        public ActionResult PostDataflowComputations() {
            DataflowComputations dataflowComputations = new DataflowComputations();

            dataflowComputations.execute();

            return Accepted();
        }
    }
}
