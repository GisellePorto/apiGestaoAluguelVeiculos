using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraAPI.Models;

public class Aluguel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int VeiculoId { get; set; }

    [ForeignKey(nameof(VeiculoId))]
    public Veiculo? Veiculo { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Cliente? Cliente { get; set; }

    [Required]
    public DateTime DataInicio { get; set; }

    [Required]
    public DateTime DataFim { get; set; }
}