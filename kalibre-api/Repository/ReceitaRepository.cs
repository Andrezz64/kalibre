using kalibre_api;

public class ReceitaRepository : IReceitaRepository
{
    private readonly KalibreContext _Dbcontext;

    public ReceitaRepository(KalibreContext dbcontext)
    {
        _Dbcontext = dbcontext;
    }

    public void Insert(Receita receita)
    {
        //receita.Data = receita.Data.AddHours(3);
        _Dbcontext.Receitas.Add(receita);
        _Dbcontext.SaveChanges();
    }

    public void Update(Receita receita)
    {
        //receita.Data = receita.Data.AddHours(3);
        _Dbcontext.Receitas.Update(receita);
        _Dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {

        Receita receita = new()
        {
            Receitaid = id
        };
        _Dbcontext.Remove(receita); // remove do banco a receita relativa ao id passado pelo objeto
        _Dbcontext.SaveChanges();

    }

    public List<Receita> GetAll()
    {
        return _Dbcontext.Receitas.OrderByDescending(receitas => receitas.Data).ToList();
    }

}