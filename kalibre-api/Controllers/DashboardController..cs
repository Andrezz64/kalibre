
using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ILogger<DespesasController> _logger;
    private readonly KalibreContext _context = new();

    public DashboardController(ILogger<DespesasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get()
    {
       var currentYear = DateTime.Now.Year;

  var Meses = Enumerable.Range(1, 12);

        var ListaDespesas = Meses
            .Select(month => new Dashboard
            {
                Mes = month,
                ValorTotal = _context.Despesas
                    .Where(d => d.Data.Year == currentYear && d.Data.Month == month)
                    .Sum(d => d.Valor) // Sum for the current month
            })
            // Add 0 for months with no expenses
            .Select(ms => new Dashboard
            {
                Mes = ms.Mes,
                ValorTotal = ms.ValorTotal == 0 ? 0 : ms.ValorTotal // Replace 0 with actual sum
            })
            .OrderBy(ms => ms.Mes)
            .ToList();

              var ListaReceitas = Meses
            .Select(month => new Dashboard
            {
                Mes = month,
                ValorTotal = _context.Receitas
                    .Where(d => d.Data.Year == currentYear && d.Data.Month == month)
                    .Sum(d => d.Valor) // Sum for the current month
            })
            // Add 0 for months with no expenses
            .Select(ms => new Dashboard
            {
                Mes = ms.Mes,
                ValorTotal = ms.ValorTotal == 0 ? 0 : ms.ValorTotal // Replace 0 with actual sum
            })
            .OrderBy(ms => ms.Mes)
            .ToList();
            



        return Ok(new { Status = "Ok", despesaTotalAno = ListaDespesas, receitaTotalAno= ListaReceitas });
    }

}