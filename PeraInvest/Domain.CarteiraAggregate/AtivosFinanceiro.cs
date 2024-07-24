using System;
using System.Collections.Generic;

namespace Domain.CarteiraAggregate;

public partial class AtivosFinanceiro
{
    public byte[] Id { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string CodigoNegociacao { get; set; } = null!;

    public decimal? CotacaoAtual { get; set; }

    public DateTime? DataAtualizacaoCotacao { get; set; }

    public string ClasseAtivo { get; set; } = null!;

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataEmissao { get; set; }

    public string? Emissor { get; set; }

    public ulong? Status { get; set; }
}
