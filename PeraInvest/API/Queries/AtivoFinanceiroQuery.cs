using Microsoft.EntityFrameworkCore;
using PeraInvest.API.Queries.ViewModels;
using PeraInvest.Infrastructure;

namespace PeraInvest.API.Queries {
    public class AtivoFinanceiroQuery {
        private readonly AtivoFinanceiroContext context;

        public AtivoFinanceiroQuery(AtivoFinanceiroContext context) {
            this.context = context;
        }

        public async Task<AtivoFinanceiroViewModel> ObterAtivoPorCodigoNegociacao(string codigo) {
            var ativo = await context.AtivosFinanceiro
                .Where(a => a.CodigoNegociacao == codigo)
                .FirstOrDefaultAsync() ?? throw new Exception("Não achou");

            return new AtivoFinanceiroViewModel {
                Id = new Guid(ativo.Id).ToString(),
                Nome = ativo.Nome,
                Descricao = ativo.Descricao,
                CodigoNegociacao = ativo.CodigoNegociacao,
                Index = ativo.Index,
                ClasseAtivo = ativo.ClasseAtivo.ToString(),
                DataEmissao = ativo.DataEmissao,
                DataVencimento = ativo.DataVencimento,
                Emissor = ativo.Emissor,
                Status = ativo.Status == true ? "ATIVO" : "INATIVO"
            };
        }
    }
}
