namespace Sora_AppProject.Application.Dtos;

public class DisenoTelaDto
{
    public int Id { get; set; }
    public int DisenoId { get; set; }
    public int TelaId { get; set; }
    public string Parte { get; set; } = null!;
}