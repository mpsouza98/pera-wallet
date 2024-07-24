using System;
using System.Collections.Generic;

namespace Domain.CarteiraAggregate;

public partial class Usuario
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;
}
