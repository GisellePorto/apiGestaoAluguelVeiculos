using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Dtos;

public record ClienteCreateDto(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$", ErrorMessage = "CPF inválido. Use 11 dígitos ou o formato 000.000.000-00.")]
    string Cpf,

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    string Email,

    [Phone(ErrorMessage = "Número de telefone inválido.")]
    string? Telefone
);

public record ClienteUpdateDto(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    string Email,

    [Phone(ErrorMessage = "Número de telefone inválido.")]
    string? Telefone
);