using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly ILogger<RelatorioController> _logger;

    private readonly IRelatorioRepository _repository; // Inicializa a classe responsável pelas querys
    public RelatorioController(ILogger<RelatorioController> logger, IRelatorioRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    [HttpPost()]
    public IActionResult Post([FromBody] Relatorio relatorio)
    {
        try
        {
            Object DespesaAnalitycs = _repository.AnaliseDespesaEmPeriodo(relatorio.DataInicial, relatorio.DataFinal); // Faz a analise dentro do banco baseada na data passada como argumento, retornando a soma de despesas e soma dos valores
            Object ReceitaAnalitycs = _repository.AnaliseReceitaEmPeriodo(relatorio.DataInicial, relatorio.DataFinal); // Utiliza a mesma a lógica, mas para as receitas
            return Ok(new // Retorna um objeto com os dados para o front-end
            {
                Status = "Ok",
                data = new
                {
                    DespesaAnalitycs,
                    ReceitaAnalitycs
                }
            });
        }
        catch (Exception ex)
        {
         return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }


}
