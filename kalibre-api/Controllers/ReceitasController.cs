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
        var lista = _context.Receitas.OrderByDescending(despesas => despesas.Data);

        return Ok(lista);
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Receita receita)
    {
        DateTime now = DateTime.Now;

        try
        {
            _context.Add(new Receita
            {
                Data = now,
                Valor = receita.Valor
            });
            _context.SaveChanges();
            return Ok(receita);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "ocorreu um erro interno: " + ex.Message);
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
            return NotFound("Não foi possivel localizar o objeto no banco de dados"+ex.Message);
        }
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] Receita receita){
           if (id != receita.Receitaid)
        {
            return BadRequest();
        }
        Receita ReceitaAtualizada =  new()
        {
            Receitaid =  id,
            Valor = receita.Valor,
            Data = receita.Data
        };
         _context.Receitas.Update(ReceitaAtualizada);
         _context.SaveChanges();
         return NoContent();
    }
}
