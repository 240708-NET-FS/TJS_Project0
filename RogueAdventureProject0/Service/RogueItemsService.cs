using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Service;

public class RogueItemsService : IService<RogueItems>
{
    private readonly RogueItemsDAO _ItemDAO;

    public RogueItemsService(RogueItemsDAO dao)
    {
        _ItemDAO = dao;
    }

    
    public void Create(RogueItems item)
    {
        _ItemDAO.Create(item);
    }

    public void Delete(RogueItems item)
    {
        _ItemDAO.Delete(item);
    }

    public ICollection<RogueItems> GetAll()
    {
        List<RogueItems> rogueAccounts = _ItemDAO.GetAll().ToList();

        return rogueAccounts;
    }

    public RogueItems GetByID(int Id)
    {
        return _ItemDAO.GetByID(Id);
    }

    public void Update(RogueItems item)
    {
        _ItemDAO.Update(item);
    }
}