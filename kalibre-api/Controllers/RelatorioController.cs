using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly ILogger<RelatorioController> _logger;
    private readonly KalibreContext _context = new();
    private readonly DbDespesaReceitaAnalitycs _Analitycs = new();
    public RelatorioController(ILogger<RelatorioController> logger)
    {
        _logger = logger;
    }


    [HttpPost()]
    public IActionResult Post([FromBody] Relatorio relatorio)
    {
        try
        {
            var DespesaAnalitycs = _Analitycs.AnaliseDespesaEmPeriodo(relatorio.DataInicial, relatorio.DataFinal);
            var ReceitaAnalitycs = _Analitycs.AnaliseReceitaEmPeriodo(relatorio.DataInicial, relatorio.DataFinal);
            return Ok(new
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
            Console.WriteLine(ex);
            return StatusCode(500, new { status = "Error", msg = ex.Message });
        }

    }


}
