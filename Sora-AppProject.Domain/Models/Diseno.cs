using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class Diseno
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int PrendaBaseId { get; set; }

    public string Nombre { get; set; } = null!;

    public string ConfiguracionVisor { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario Cliente { get; set; } = null!;

    public virtual ICollection<DisenoTela> DisenoTelas { get; set; } = new List<DisenoTela>();

    public virtual PrendasBase PrendaBase { get; set; } = null!;

    public virtual ICollection<SolicitudesCotizacion> SolicitudesCotizacions { get; set; } = new List<SolicitudesCotizacion>();
}
