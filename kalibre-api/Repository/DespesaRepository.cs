using kalibre_api;

public class DespesaRepository : IDespesaRepository
{
    private readonly KalibreContext _Dbcontext;

    public DespesaRepository(KalibreContext dbcontext)
    {
        _Dbcontext = dbcontext;
    }

    public void Insert(Despesa despesa)
    {
       // despesa.Data.AddHours(3);
        _Dbcontext.Despesas.Add(despesa);
        _Dbcontext.SaveChanges();
    }

    public void Update(Despesa despesa)
    {
       // despesa.Data.AddHours(3);
        _Dbcontext.Despesas.Update(despesa);
        _Dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {

        Despesa despesa = new()
        {
            DespesaId = id
        };
        _Dbcontext.Remove(despesa); // remove do banco a despesa relativa ao id passado pelo objeto
        _Dbcontext.SaveChanges();

    }

    public List<Despesa> GetAll()
    {
        return _Dbcontext.Despesas.OrderByDescending(despesas => despesas.Data).ToList();
    }

}