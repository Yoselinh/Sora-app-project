namespace Sora_AppProject.Application.Dtos;

public class SolicitudTallaDto
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public string Talla { get; set; } = null!;
    public int Cantidad { get; set; }
}
