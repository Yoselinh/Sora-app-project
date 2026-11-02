using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class DisenoTela
{
    public int Id { get; set; }

    public int DisenoId { get; set; }

    public int TelaId { get; set; }

    public string Parte { get; set; } = null!;

    public virtual Diseno Diseno { get; set; } = null!;

    public virtual Tela Tela { get; set; } = null!;
}
