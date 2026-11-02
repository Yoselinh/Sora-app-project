using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class Cotizacione
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }

    public int TallerId { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Total { get; set; }

    public int PlazoDias { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual SolicitudesCotizacion Solicitud { get; set; } = null!;

    public virtual Tallere Taller { get; set; } = null!;
}
