using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceitasController : ControllerBase // Controlador da rota de despesa
{
    private readonly ILogger<ReceitasController> _logger;


    private readonly IReceitaRepository _repository;

    public ReceitasController(ILogger<ReceitasController> logger, IReceitaRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet()]
    public IActionResult Get() // Obtem as receitas presentes no banco e retorna para o front-end
    {
        List<Receita> lista = _repository.GetAll(); // Selecioando a entidade, ordenando pelas mais recentes

        return Ok(new { Status = "Ok", data = lista });
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Receita receita) // Criar uma nova receita apartir de um POST
    {
        try
        {
            _repository.Insert(receita);
            return Ok(new { Status = "Ok", data = receita });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }
    [HttpDelete()]
    [Route("{id}")]
    public IActionResult Delete(int id) // apaga uma receita do banco de dados, apartir de um ID presente na url
    {
        try
        {
            
            _repository.Delete(id);

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

            _repository.Update(receita);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(501, new { status = "Error", msg = ex.Message });
        }
    }
}
