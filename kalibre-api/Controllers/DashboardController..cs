using Microsoft.AspNetCore.Mvc;

namespace kalibre_api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase //Controlador da rota Dashboard
{
    private readonly ILogger<DespesasController> _logger;
    private readonly KalibreContext _context = new();

    public DashboardController(ILogger<DespesasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get() // Rota para os gráficos do front, envia os dados como Obejto.
    {
        var AnoAtual = DateTime.Now.Year;

        var Meses = Enumerable.Range(1, 12);

        var ListaDespesas = Meses
            .Select(month => new Dashboard // Inicia o Select baseado na classe de Dashboard
            {
                Mes = month,
                ValorTotal = _context.Despesas
                    .Where(d => d.Data.Year == AnoAtual && d.Data.Month == month) // Logica para selecionar dados dentro do ano atual
                    .Sum(d => d.Valor) // Soma os valores das despesas mensalmente
            })

            .Select(ms => new Dashboard 
            {
                Mes = ms.Mes,
                ValorTotal = ms.ValorTotal == 0 ? 0 : ms.ValorTotal // Lógica para atribuir as somas aos respectivos meses, se não possui nenhum valor se mantem como 0
            })
            .OrderBy(ms => ms.Mes) // Ordena os meses pelo numero, de 1 a 12
            .ToList();

        
        var ListaReceitas = Meses // Realiza a mesma logica mas com as receitas, visto que possuem os mesmos atributos
      .Select(month => new Dashboard
      {
          Mes = month,
          ValorTotal = _context.Receitas
              .Where(d => d.Data.Year == AnoAtual && d.Data.Month == month)
              .Sum(d => d.Valor) 
      })
     
      .Select(ms => new Dashboard 
      {
          Mes = ms.Mes,
          ValorTotal = ms.ValorTotal == 0 ? 0 : ms.ValorTotal
      })
      .OrderBy(ms => ms.Mes)
      .ToList();
        
      return Ok(new { Status = "Ok", despesaTotalAno = ListaDespesas, receitaTotalAno = ListaReceitas });  // Retorna o obejto com as listas, despesa e receitas.
    }

}