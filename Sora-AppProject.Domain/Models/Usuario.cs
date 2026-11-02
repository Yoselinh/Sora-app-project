using System;
using System.Collections.Generic;

namespace Sora_AppProject.Domain.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RolId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Diseno> Disenos { get; set; } = new List<Diseno>();

    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<SolicitudesCotizacion> SolicitudesCotizacions { get; set; } = new List<SolicitudesCotizacion>();

    public virtual Tallere? Tallere { get; set; }
}
