namespace Sora_AppProject.Application.Dtos;

public class TallerTelaDto
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
    public string TelaNombre { get; set; } = null!;
    public decimal PrecioPorMetro { get; set; }
}