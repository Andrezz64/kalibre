namespace kalibre_api.Controllers;
class DbDespesaReceitaAnalitycs
{
    private readonly KalibreContext _context = new();

    public object AnaliseDespesaEmPeriodo(DateTime DataInicial, DateTime DataFinal)
    {
        decimal ValorDespesa = 0;
        var Despesas = _context.Despesas
        .Where(order => order.Data >= new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day) && order.Data <= new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day))
        .ToList();
        foreach (var despesa in Despesas)
        {
            ValorDespesa += despesa.Valor;
        }
        int QuantidadeDespesa = Despesas.Count;
        return new
        {
            QuantidadeDespesa,
            ValorDespesa,
        };
    }
    public object AnaliseReceitaEmPeriodo(DateTime DataInicial, DateTime DataFinal)
    {

        decimal ValorReceita = 0;
        var Receitas = _context.Receitas
        .Where(order => order.Data >= new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day) && order.Data <= new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day))
        .ToList();
        foreach (var receita in Receitas)
        {
            ValorReceita += receita.Valor;
        }
        int QuantidadeReceita = Receitas.Count;
        return new
        {
            QuantidadeReceita,
            ValorReceita,
        };
    }
}