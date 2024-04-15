using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesasController : ControllerBase
{
    private readonly ILogger<DespesasController> _logger;
    private readonly KalibreContext _context = new();

    public DespesasController(ILogger<DespesasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get()
    {
        var lista = _context.Despesas.OrderByDescending(despesas => despesas.Data);
        var response = new{Status="Ok",data=lista};
        return Ok(response);
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Despesa despesa)
    {
        DateTime now = DateTime.Now;

        try
        {
            _context.Add(new Despesa
            {   
                Data = despesa.Data.ToLocalTime(),
                Valor = despesa.Valor
            });
            _context.SaveChanges();
            return Ok(new {Status="Ok",data=despesa});
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return StatusCode(500, new {status="Error",msg=ex.Message});
        }

    }
    [HttpDelete()]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            Despesa despesa = new()
            {
                DespesaId = id
            };
            _context.Remove(despesa);
            _context.SaveChanges();

            return StatusCode(204);
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex){
            return NotFound("Não foi possivel localizar o objeto no banco de dados"+ex.Message);
        }
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] Despesa despesa){
           if (id != despesa.DespesaId)
        {
            return BadRequest();
        }
    
        Despesa DespesaAtualizada =  new()
        {
            DespesaId =  id,
            Valor = despesa.Valor,
            Data = despesa.Data.ToLocalTime(),
          
        };
         _context.Despesas.Update(DespesaAtualizada);
         _context.SaveChanges();
         return NoContent();
    }
}
