namespace Sora_AppProject.Application.Dtos;

public class CotizacionDto
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public int TallerId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public int PlazoDias { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime Fecha { get; set; }
}