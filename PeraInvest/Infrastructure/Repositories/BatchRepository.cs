using Microsoft.EntityFrameworkCore;
using PeraInvest.API.Batch.Models;
using PeraInvest.Domain.SeedWork;
using System.Drawing;
using System.Security.Policy;
using static PeraInvest.API.Batch.Models.Rotina;

namespace PeraInvest.Infrastructure.Repositories {
    public interface IBatchRepository : IRepository<BlocoProcessamento> {
        public Task InsereBlocos(List<BlocoProcessamento> blocos);
        public Task<List<BlocoProcessamento>> ConsultaBlocosDisponiveisRotina(string rotinaId);
        public Task<Rotina> ConsultaRotina(string rotinaId);
        public Task AlteraEstadoBloco(int blocoId, Estado novoEstado);
        public Task AtualizaRotina(Rotina rotina);
        public Task<BlocoProcessamento> ObterBloco(int blocoId);
        public Task ExpurgoBlocos(string rotinaId);

        //public Task ConsultaTamanhoArmazenamentoTabela(string tabela);

    }
    public class BatchRepository : IBatchRepository {
        private readonly BatchContext context;

        public IUnitOfWork UnitOfWork {
            get {
                return context;
            }
        }
        public BatchRepository(BatchContext context) {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<BlocoProcessamento>> ConsultaBlocosDisponiveisRotina(string rotinaId) {
            return context.BlocosProcessamento
                .Where(a => a.RotinaId.Equals(rotinaId) && a.Estado.Equals(Estado.NAO_INICIADO))
                .ToListAsync();
        }

        public async Task InsereBlocos(List<BlocoProcessamento> blocos) {
            await context.BlocosProcessamento.AddRangeAsync(blocos);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<BlocoProcessamento> ObterBloco(int blocoId) {
            return await context.BlocosProcessamento
                .Where(a => a.Id.Equals(blocoId))
                .SingleAsync();
        }

        public async Task ExpurgoBlocos(string rotinaId) {
            var entities = context.BlocosProcessamento
                .Where(b => b.RotinaId.Equals(rotinaId))
                .ToList();

            context.BlocosProcessamento.RemoveRange(entities);

            await context.SaveChangesAsync();
        }

        public async Task<Rotina> ConsultaRotina(string rotinaId) {
            return await context.Rotina
                .Where(a => a.Id.Equals(rotinaId))
                .SingleAsync();
        }

        public async Task AlteraEstadoBloco(int blocoId, Estado novoEstado) {
            await context.BlocosProcessamento
                .Where(a => a.Id == blocoId)
                .ExecuteUpdateAsync(o => o.SetProperty(p => p.Estado, novoEstado));

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task AtualizaRotina(Rotina rotina) {
            var result = await context.Rotina
                .Where(a => a.Id.Equals(rotina.Id))
                .SingleAsync();

            result.EstadoExecucao = rotina.EstadoExecucao;
            result.DataInicioProcessamento = rotina.DataInicioProcessamento;
            result.DataFimProcessamento = rotina.DataFimProcessamento;

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
