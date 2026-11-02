using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class SolicitudesCotizacion
{
    public int Id { get; set; }

    public int DisenoId { get; set; }

    public int ClienteId { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual Usuario Cliente { get; set; } = null!;

    public virtual ICollection<Cotizacione> Cotizaciones { get; set; } = new List<Cotizacione>();

    public virtual Diseno Diseno { get; set; } = null!;

    public virtual ICollection<SolicitudTalla> SolicitudTallas { get; set; } = new List<SolicitudTalla>();
}
