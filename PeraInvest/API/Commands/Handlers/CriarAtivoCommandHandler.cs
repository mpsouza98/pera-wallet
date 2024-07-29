using MediatR;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;

namespace PeraInvest.API.Commands.Handlers {
    public class CriarAtivoCommandHandler : IRequestHandler<CriarAtivoCommand, AtivoFinanceiro> {
        private readonly IAtivoFinanceiroRepository _ativoFinanceiroRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CriarAtivoCommandHandler> _logger;

        public CriarAtivoCommandHandler(IAtivoFinanceiroRepository ativoFinanceiroRepository, IMediator mediator, ILogger<CriarAtivoCommandHandler> logger) {
            _ativoFinanceiroRepository = ativoFinanceiroRepository ?? throw new ArgumentNullException(nameof(ativoFinanceiroRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CriarAtivoCommand request, CancellationToken cancellationToken) {
            var ativoFinanceiro = new AtivoFinanceiro(
                Guid.NewGuid().ToString(),
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

            _logger.LogInformation("Criando Ativo Financeiro: {@AtivoFinanceiro}", ativoFinanceiro);

            _ativoFinanceiroRepository.CriarAtivo(ativoFinanceiro);

            return await _ativoFinanceiroRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }

        Task<AtivoFinanceiro> IRequestHandler<CriarAtivoCommand, AtivoFinanceiro>.Handle(CriarAtivoCommand request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
