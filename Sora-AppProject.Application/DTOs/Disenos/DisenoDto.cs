namespace Sora_AppProject.Application.DTOs.Disenos;

public record DisenoDto(
    int Id,
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor,
    DateTime FechaCreacion
);