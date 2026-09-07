using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Dtos;

public record VeiculoCreateDto(
    [Required] string Marca,
    [Required] string Modelo,
    [Range(1900, 2100)] int Ano,
    [Required] string Placa,
    [Range(0.01, double.MaxValue)] decimal ValorDiaria
);

public record VeiculoUpdateDto(
    [Required] string Marca,
    [Required] string Modelo,
    [Range(1900, 2100)] int Ano,
    [Range(0.01, double.MaxValue)] decimal ValorDiaria
);