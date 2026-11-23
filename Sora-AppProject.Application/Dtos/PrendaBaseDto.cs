namespace Sora_AppProject.Application.Dtos;

public class PrendaBaseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}
