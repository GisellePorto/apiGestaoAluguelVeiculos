using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Dtos;

public record AluguelCreateDto(
    [Required] int VeiculoId,
    [Required] int ClienteId,
    [Required] DateTime DataInicio,
    [Required] DateTime DataFim
);