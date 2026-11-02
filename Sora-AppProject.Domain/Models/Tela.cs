using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class Tela
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Composicion { get; set; }

    public virtual ICollection<DisenoTela> DisenoTelas { get; set; } = new List<DisenoTela>();

    public virtual ICollection<TallerTela> TallerTelas { get; set; } = new List<TallerTela>();
}
