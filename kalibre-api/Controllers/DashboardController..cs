using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase //Controlador da rota Dashboard
{
    private readonly ILogger<DespesasController> _logger;

    private readonly IDashboardRepository _repository;
    
    public DashboardController(ILogger<DespesasController> logger, IDashboardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet()]
    public IActionResult Get() // Rota para os gráficos do front, envia os dados como Obejto.
    {
        try
        {
            List<Dashboard> ListaDespesas = _repository.GetDespesaAnual();
            List<Dashboard> ListaReceitas = _repository.GetReceitaAnual();

            return Ok(new { Status = "Ok", despesaTotalAno = ListaDespesas, receitaTotalAno = ListaReceitas });  // Retorna o obejto com as listas, despesa e receitas.
        }
        catch (Exception ex)
        {
            return StatusCode(501, new { status = "Error", msg = ex.Message });
        }
    }

}