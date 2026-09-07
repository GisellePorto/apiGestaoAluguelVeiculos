using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraAPI.Data;
using LocadoraAPI.Dtos;
using LocadoraAPI.Models;

namespace LocadoraAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlugueisController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlugueisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Aluguel>> Create(AluguelCreateDto dto)
    {
        if (dto.DataFim <= dto.DataInicio)
            return BadRequest(new { message = "A data final deve ser posterior à data inicial." });

        var veiculo = await _context.Veiculos.FindAsync(dto.VeiculoId);
        if (veiculo == null)
            return NotFound(new { message = "Veículo não encontrado." });

        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        if (!veiculo.Disponivel)
            return BadRequest(new { message = "Veículo indisponível para locação." });

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var aluguel = new Aluguel
            {
                VeiculoId = dto.VeiculoId,
                ClienteId = dto.ClienteId,
                DataInicio = dto.DataInicio.ToUniversalTime(),
                DataFim = dto.DataFim.ToUniversalTime()
            };

            veiculo.Disponivel = false;

            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return StatusCode(201, aluguel);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, new { message = "Erro ao processar o aluguel." });
        }
    }
}