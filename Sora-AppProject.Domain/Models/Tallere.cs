using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class Tallere
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Cotizacione> Cotizaciones { get; set; } = new List<Cotizacione>();

    public virtual ICollection<TallerTela> TallerTelas { get; set; } = new List<TallerTela>();

    public virtual Usuario Usuario { get; set; } = null!;
}
