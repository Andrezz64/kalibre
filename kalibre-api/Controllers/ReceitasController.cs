using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceitasController : ControllerBase
{
    private readonly ILogger<ReceitasController> _logger;
    private readonly KalibreContext _context = new();

    public ReceitasController(ILogger<ReceitasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get()
    {
        var lista = _context.Receitas.OrderByDescending(receitas => receitas.Data);

      return Ok(new{Status="Ok",data=lista});
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Receita receita)
    {
        try
        {
            _context.Add(new Receita
            {
                Data = receita.Data.AddHours(3).ToLocalTime(),
                Valor = receita.Valor
            });
            _context.SaveChanges();
            return Ok(new { Status = "Ok", data = receita });
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }
    [HttpDelete()]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            Receita receita = new()
            {
                Receitaid = id
            };
            _context.Remove(receita);
            _context.SaveChanges();

            return StatusCode(204);
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex){
            return NotFound(new{status="Error",message=ex.Message});
        }
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] Receita receita){
           if (id != receita.Receitaid)
        {
            return BadRequest();
        }

        Receita ReceitaAtualizada = new()
        {
            Receitaid = id,
            Valor = receita.Valor,
            Data = receita.Data.AddHours(3).ToLocalTime(),

        };
        _context.Receitas.Update(ReceitaAtualizada);
        _context.SaveChanges();
        return NoContent();
    }
}
