namespace Sora_AppProject.Application.Dtos;

public class SolicitudCotizacionDto
{
    public int Id { get; set; }
    public int DisenoId { get; set; }
    public int ClienteId { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime Fecha { get; set; }
}
