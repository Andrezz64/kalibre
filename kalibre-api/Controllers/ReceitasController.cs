using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceitasController : ControllerBase // Controlador da rota de despesa
{
    private readonly ILogger<ReceitasController> _logger;
    private readonly KalibreContext _context = new();

    public ReceitasController(ILogger<ReceitasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get() // Obtem as receitas presentes no banco e retorna para o front-end
    {
        var lista = _context.Receitas.OrderByDescending(receitas => receitas.Data); // Selecioando a entidade, ordenando pelas mais recentes

        return Ok(new { Status = "Ok", data = lista });
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Receita receita) // Criar uma nova receita apartir de um POST
    {
        try
        {
            _context.Add(new Receita // Inicializa uma nova entidade, na forma de classe cs, que será usada como referencia para o entity
            {
                Data = receita.Data.AddHours(3).ToLocalTime(), // Ajusta para o hórario brasileiro, apartir do hórário do pacifico
                Valor = receita.Valor
            });
            _context.SaveChanges(); // Salva a nova entry no banco de dados
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
    public IActionResult Delete(int id) // apaga uma receita do banco de dados, apartir de um ID presente na url
    {
        try
        {
            Receita receita = new()
            {
                Receitaid = id
            };
            _context.Remove(receita); // remove do banco a receita relativa ao id passado pelo objeto
            _context.SaveChanges();

            return StatusCode(204);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            return NotFound(new { status = "Error", message = ex.Message });
        }
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] Receita receita) // Altera uma receita no banco de dados
    {
        try
        {
            if (id != receita.Receitaid) // valida se o ID passado na URL é igual ao presente no Body
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
        catch (Exception ex)
        {
            return StatusCode(501, new { status = "Error", msg = ex.Message });
        }
    }
}
