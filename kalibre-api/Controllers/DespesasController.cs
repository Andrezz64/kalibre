using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesasController : ControllerBase // Controlador da rota de despesa
{
    private readonly ILogger<DespesasController> _logger;
    private readonly KalibreContext _context = new();

    public DespesasController(ILogger<DespesasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get() // Obtem as despesas presentes no banco e retorna para o front-end
    {
        var lista = _context.Despesas.OrderByDescending(despesas => despesas.Data); // Selecioando a entidade, ordenando pelas mais recentes

        return Ok(new { Status = "Ok", data = lista });
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Despesa despesa) // Criar uma nova despesa apartir de um POST
    {
        try
        {
            _context.Add(new Despesa // Inicializa uma nova entidade, na forma de classe cs, que será usada como referencia para o entity
            {
                Data = despesa.Data.AddHours(3).ToLocalTime(), // Ajusta para o hórario brasileiro, apartir do hórário do pacifico
                Valor = despesa.Valor
            });
            _context.SaveChanges(); // Salva a nova entry no banco de dados
            return Ok(new { Status = "Ok", data = despesa });
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }
    [HttpDelete()]
    [Route("{id}")]
    public IActionResult Delete(int id) // apaga uma despesa do banco de dados, apartir de um ID presente na url
    {
        try
        {
            Despesa despesa = new()
            {
                DespesaId = id
            };
            _context.Remove(despesa); // remove do banco a despesa relativa ao id passado pelo objeto
            _context.SaveChanges();

            return StatusCode(204);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            return NotFound("Não foi possivel localizar o objeto no banco de dados" + ex.Message);
        }
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] Despesa despesa) // Altera uma despesa no banco de dados
    {
        try
        {
            if (id != despesa.DespesaId) // valida se o ID passado na URL é igual ao presente no Body
            {
                return BadRequest();
            }

            Despesa DespesaAtualizada = new()
            {
                DespesaId = id,
                Valor = despesa.Valor,
                Data = despesa.Data.AddHours(3).ToLocalTime(),

            };
            _context.Despesas.Update(DespesaAtualizada);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(501, new { status = "Error", msg = ex.Message });
        }
    }
}
