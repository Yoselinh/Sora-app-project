using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class PrendasBase
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Modelo3dUrl { get; set; }

    public virtual ICollection<Diseno> Disenos { get; set; } = new List<Diseno>();
}
