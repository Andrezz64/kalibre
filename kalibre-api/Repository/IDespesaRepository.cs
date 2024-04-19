using kalibre_api;

public interface IDespesaRepository
{
    List<Despesa> GetAll();

    void Insert(Despesa despesa);
    void Update(Despesa despesa);
    void Delete(int id);

}