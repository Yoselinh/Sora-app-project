namespace Sora_AppProject.Application.Dtos.PrendasBase;

public class CreatePrendaBaseRequestDto
{
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}

public class UpdatePrendaBaseRequestDto
{
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}

public class PrendaBaseResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}
