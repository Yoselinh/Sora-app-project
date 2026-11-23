namespace Sora_AppProject.Application.Dtos.Telas;

public class CreateTelaRequestDto
{
    public string Nombre { get; set; } = null!;
    public string? Composicion { get; set; }
}

public class UpdateTelaRequestDto
{
    public string Nombre { get; set; } = null!;
    public string? Composicion { get; set; }
}

public class TelaResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Composicion { get; set; }
}
