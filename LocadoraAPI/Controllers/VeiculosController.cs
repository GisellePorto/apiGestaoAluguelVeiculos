using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraAPI.Data;
using LocadoraAPI.Dtos;
using LocadoraAPI.Models;

namespace LocadoraAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly AppDbContext _context;

    public VeiculosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Veiculo>>> GetAll()
    {
        return await _context.Veiculos.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Veiculo>> GetById(int id)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);
        if (veiculo == null) return NotFound(new { message = "Veículo não encontrado." });
        return Ok(veiculo);
    }

    [HttpGet("placa/{placa}")]
    public async Task<ActionResult<Veiculo>> GetByPlaca(string placa)
    {
        var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa.ToUpper());
        if (veiculo == null) return NotFound(new { message = "Veículo não encontrado." });
        return Ok(veiculo);
    }

    [HttpPost]
    public async Task<ActionResult<Veiculo>> Create(VeiculoCreateDto dto)
    {
        var placaExiste = await _context.Veiculos.AnyAsync(v => v.Placa == dto.Placa.ToUpper());
        if (placaExiste) return BadRequest(new { message = "Já existe um veículo com esta placa." });

        var veiculo = new Veiculo
        {
            Marca = dto.Marca,
            Modelo = dto.Modelo,
            Ano = dto.Ano,
            Placa = dto.Placa.ToUpper(),
            ValorDiaria = dto.ValorDiaria,
            Disponivel = true
        };

        _context.Veiculos.Add(veiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, veiculo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VeiculoUpdateDto dto)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);
        if (veiculo == null) return NotFound(new { message = "Veículo não encontrado." });

        veiculo.Marca = dto.Marca;
        veiculo.Modelo = dto.Modelo;
        veiculo.Ano = dto.Ano;
        veiculo.ValorDiaria = dto.ValorDiaria;

        await _context.SaveChangesAsync();
        return Ok(veiculo);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);
        if (veiculo == null) return NotFound(new { message = "Veículo não encontrado." });

        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}