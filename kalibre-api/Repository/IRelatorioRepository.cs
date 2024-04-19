using kalibre_api;

public interface IRelatorioRepository{
    object AnaliseDespesaEmPeriodo(DateTime DataInicial, DateTime DataFinal);
    object AnaliseReceitaEmPeriodo(DateTime DataInicial, DateTime DataFinal);
}