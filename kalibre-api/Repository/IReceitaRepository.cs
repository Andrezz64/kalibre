using kalibre_api;

public interface IReceitaRepository
{
    List<Receita> GetAll();

    void Insert(Receita receita);
    void Update(Receita receita);
    void Delete(int id);

}