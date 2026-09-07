using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraAPI.Data;
using LocadoraAPI.Dtos;
using LocadoraAPI.Models;

namespace LocadoraAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        return Ok(cliente);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<Cliente>> GetByCpf(string cpf)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado com o CPF informado." });

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Create(ClienteCreateDto dto)
    {
        var cpfExiste = await _context.Clientes.AnyAsync(c => c.Cpf == dto.Cpf);
        if (cpfExiste)
            return BadRequest(new { message = "Já existe um cliente com este CPF." });

        var emailExiste = await _context.Clientes.AnyAsync(c => c.Email.ToLower() == dto.Email.ToLower());
        if (emailExiste)
            return BadRequest(new { message = "Já existe um cliente com este e-mail." });

        var novoCliente = new Cliente
        {
            Nome = dto.Nome,
            Cpf = dto.Cpf,
            Email = dto.Email.ToLower(),
            Telefone = dto.Telefone
        };

        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, novoCliente);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ClienteUpdateDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        var emailEmUso = await _context.Clientes.AnyAsync(c => c.Email.ToLower() == dto.Email.ToLower() && c.Id != id);
        if (emailEmUso)
            return BadRequest(new { message = "Este e-mail já está sendo utilizado por outro cliente." });

        cliente.Nome = dto.Nome;
        cliente.Email = dto.Email.ToLower();
        cliente.Telefone = dto.Telefone;

        await _context.SaveChangesAsync();
        return Ok(cliente);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        var possuiHistoricoAluguel = await _context.Alugueis.AnyAsync(a => a.ClienteId == id);
        if (possuiHistoricoAluguel)
            return BadRequest(new { message = "Não é possível excluir um cliente que possui histórico de aluguéis." });

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}