using MediatR;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;

namespace PeraInvest.API.Commands.Handlers {
    public class CriarAtivoCommandHandler : IRequestHandler<CriarAtivoCommand, AtivoFinanceiro> {
        private readonly IAtivoFinanceiroRepository ativoFinanceiroRepository;
        private readonly ILogger<CriarAtivoCommandHandler> logger;

        public CriarAtivoCommandHandler(IAtivoFinanceiroRepository ativoFinanceiroRepository, ILogger<CriarAtivoCommandHandler> logger) {
            this.ativoFinanceiroRepository = ativoFinanceiroRepository ?? throw new ArgumentNullException(nameof(ativoFinanceiroRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AtivoFinanceiro> Handle(CriarAtivoCommand request, CancellationToken cancellationToken) {
            var ativoFinanceiro = new AtivoFinanceiro(
                Guid.NewGuid().ToByteArray(),
                request.Nome,
                request.Descricao,
                request.CodigoNegociacao,
                request.Index,
                request.ClasseAtivo,
                request.DataVencimento,
                request.DataEmissao,
                request.Emissor,
                request.Status
            );

            logger.LogInformation("Criando Ativo Financeiro: {@AtivoFinanceiro}", ativoFinanceiro.Nome.ToString());

            var result = ativoFinanceiroRepository.CriarAtivo(ativoFinanceiro);

            await ativoFinanceiroRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}
