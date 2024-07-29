using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Service;

public class RogueInventoryService : IService<RogueInventory>
{
    private readonly RogueInventoryDAO _InvDAO;

    public RogueInventoryService(RogueInventoryDAO dao)
    {
        _InvDAO = dao;
    }

    public void Create(RogueInventory item)
    {
        _InvDAO.Create(item);
    }

    public void Delete(RogueInventory item)
    {
        _InvDAO.Delete(item);
    }

    public ICollection<RogueInventory> GetAll()
    {
        return _InvDAO.GetAll();
    }

    public ICollection<RogueInventory> GetAllByCID(int CID)
    {
        return _InvDAO.GetAllByCID(CID);
    }

    public RogueInventory GetByID(int Id)
    {
        return _InvDAO.GetByID(Id);
    }

    public void Update(RogueInventory item)
    {
        _InvDAO.Update(item);
    }
}