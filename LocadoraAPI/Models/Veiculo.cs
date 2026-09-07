using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraAPI.Models;

public class Veiculo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Marca { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Modelo { get; set; } = string.Empty;

    [Required]
    public int Ano { get; set; }

    [Required]
    [MaxLength(10)]
    public string Placa { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorDiaria { get; set; }

    public bool Disponivel { get; set; } = true;
}