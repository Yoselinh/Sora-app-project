namespace Sora_AppProject.Application.Dtos;

public class DisenoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int PrendaBaseId { get; set; }
    public string Nombre { get; set; } = null!;
    public string ConfiguracionVisor { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
}