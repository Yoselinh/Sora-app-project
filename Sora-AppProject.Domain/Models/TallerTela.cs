using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class TallerTela
{
    public int TallerId { get; set; }

    public int TelaId { get; set; }

    public decimal PrecioPorMetro { get; set; }

    public virtual Tallere Taller { get; set; } = null!;

    public virtual Tela Tela { get; set; } = null!;
}
