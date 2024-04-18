// Classe dedicada a analise do banco para relatorios

namespace kalibre_api.Controllers;
class DbDespesaReceitaAnalitycs
{
    private readonly KalibreContext _context = new();

    public object AnaliseDespesaEmPeriodo(DateTime DataInicial, DateTime DataFinal)  // Faz a analise dentro do banco baseada na data passada como argumento, retornando a soma de despesas e soma dos valores
    {
        decimal ValorDespesa = 0; // Inicia a variável que receberá a soma das despesas.
        var Despesas = _context.Despesas // realiza a busca no banco dentro do periodo passado como argumento, retornando as entidades no formato de um array
        .Where(order => order.Data >= new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day) && order.Data <= new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day))
        .ToList();

        foreach (var despesa in Despesas) // Laço para cada despesa retornada pelo Banco
        {
            ValorDespesa += despesa.Valor;  // Soma o valor das despesas à variavel criada anteriormente
        }
        int QuantidadeDespesa = Despesas.Count; // Obtem a quantidade de despesas apartir da lista retornada peplo banco de dados
        return new
        {
            QuantidadeDespesa,
            ValorDespesa,
        };
    }
    public object AnaliseReceitaEmPeriodo(DateTime DataInicial, DateTime DataFinal) // Faz a analise dentro do banco baseada na data passada como argumento, retornando a soma de receitas e soma dos valores
    {

        decimal ValorReceita = 0; // Inicia a variável que receberá a soma das receitas.
        var Receitas = _context.Receitas // realiza a busca no banco dentro do periodo passado como argumento, retornando as entidades no formato de um array
        .Where(order => order.Data >= new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day) && order.Data <= new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day))
        .ToList();
        
        foreach (var receita in Receitas) // Laço para cada Receita retornada pelo Banco
        {
            ValorReceita += receita.Valor; // Soma o valor das receitas à variavel criada anteriormente
        }
        int QuantidadeReceita = Receitas.Count; // Obtem a quantidade de receitas apartir da lista retornada peplo banco de dados
        return new
        {
            QuantidadeReceita,
            ValorReceita,
        };
    }
}