using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class SolicitudTalla
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }

    public string Talla { get; set; } = null!;

    public int Cantidad { get; set; }

    public virtual SolicitudesCotizacion Solicitud { get; set; } = null!;
}
