using AutoMapper;
using MediatR;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Exceptions;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using static PeraInvest.Domain.CarteiraAggregate.AtivoFinanceiro;

namespace PeraInvest.API.Commands.Handlers {
    public class CriarAtivoCommandHandler : IRequestHandler<CriarAtivoCommand, CriarAtivoFinanceiroResponse> {
        private readonly IAtivoFinanceiroRepository ativoFinanceiroRepository;
        private readonly ILogger<CriarAtivoCommandHandler> logger;
        private readonly IMapper mapper;

        public CriarAtivoCommandHandler(IAtivoFinanceiroRepository ativoFinanceiroRepository, ILogger<CriarAtivoCommandHandler> logger, IMapper mapper) {
            this.ativoFinanceiroRepository = ativoFinanceiroRepository ?? throw new ArgumentNullException(nameof(ativoFinanceiroRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CriarAtivoFinanceiroResponse> Handle(CriarAtivoCommand request, CancellationToken cancellationToken) {
            var ativoExistente = await ativoFinanceiroRepository.IsAtivoExistente(request.CodigoNegociacao);

            if (ativoExistente) throw new EntityAlreadyExistsException($"AtivoFinanceiro ja foi cadastrado! codigo={request.CodigoNegociacao}");

            var ativoFinanceiro = new AtivoFinanceiro(
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

            var reponse = mapper.Map<CriarAtivoFinanceiroResponse>(result);

            return mapper.Map<CriarAtivoFinanceiroResponse>(result);
        }

    }

    public record CriarAtivoFinanceiroResponse(
        string Id,
        string Nome,
        string Descricao,
        string CodigoNegociacao,
        decimal? Index,
        ClassesAtivo ClasseAtivo,
        DateTime? DataVencimento,
        DateTime? DataEmissao,
        string? Emissor,
        bool Status);
}
