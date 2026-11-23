namespace Sora_AppProject.Application.Dtos.Cotizaciones;

public class CreateCotizacionRequestDto
{
    public int SolicitudId { get; set; }
    public int TallerId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public int PlazoDias { get; set; }
    public string Estado { get; set; } = null!;
}

public class UpdateCotizacionRequestDto
{
    public int SolicitudId { get; set; }
    public int TallerId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public int PlazoDias { get; set; }
    public string Estado { get; set; } = null!;
}

public class CotizacionResponseDto
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
