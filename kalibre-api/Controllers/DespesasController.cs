using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesasController : ControllerBase // Controlador da rota de despesa
{
    private readonly ILogger<DespesasController> _logger;

    private readonly IDespesaRepository _repository;
  

    public DespesasController(ILogger<DespesasController> logger, IDespesaRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet()]
    public IActionResult Get() // Obtem as despesas presentes no banco e retorna para o front-end
    {
        List<Despesa> lista = _repository.GetAll(); // Selecioando a entidade, ordenando pelas mais recentes

        return Ok(new { Status = "Ok", data = lista });
    }

    [HttpPost()]
    public IActionResult Post([FromBody] Despesa despesa) // Criar uma nova despesa apartir de um POST
    {
        try
        {
            _repository.Insert(despesa);
            return Ok(new { Status = "Ok", data = despesa });
        }
        catch (Exception ex)
        {
        
            return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }
    [HttpDelete()]
    [Route("{id}")]
    public IActionResult Delete(int id) // apaga uma despesa do banco de dados, apartir de um ID presente na url
    {
        try
        {
            _repository.Delete(id);
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

            _repository.Update(despesa);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(501, new { status = "Error", msg = ex.Message });
        }
    }
}
