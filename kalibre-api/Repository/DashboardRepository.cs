using kalibre_api;

public class DashboardRepository : IDashboardRepository
{
    private readonly KalibreContext _Dbcontext;
    private readonly int AnoAtual = DateTime.Now.Year;
    private readonly IEnumerable<int> Meses = Enumerable.Range(1, 12);

    public DashboardRepository(KalibreContext dbcontext)
    {

        _Dbcontext = dbcontext;
    }
    public List<Dashboard> GetDespesaAnual()
    {
        List<Dashboard> ListaDespesas = Meses
             .Select(month => new Dashboard // Inicia o Select baseado na classe de Dashboard
             {
                 Mes = month,
                 ValorTotal = _Dbcontext.Despesas
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

        return ListaDespesas;
    }

    public List<Dashboard> GetReceitaAnual()
    {
        List<Dashboard> ListaReceitas = Meses // Realiza a mesma logica mas com as receitas, visto que possuem os mesmos atributos
        .Select(month => new Dashboard
        {
            Mes = month,
            ValorTotal = _Dbcontext.Receitas
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

        return ListaReceitas;
    }


}