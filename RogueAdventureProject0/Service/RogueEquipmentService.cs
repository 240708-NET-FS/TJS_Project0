using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Service;

public class RogueEquipmentService : IService<RogueEquipment>
{
    private readonly RogueEquipmentDAO _EquipDAO;

    public RogueEquipmentService(RogueEquipmentDAO dao)
    {
        _EquipDAO = dao;
    }

    
    public void Create(RogueEquipment item)
    {
        _EquipDAO.Create(item);
    }

    public void Delete(RogueEquipment item)
    {
        _EquipDAO.Delete(item);
    }

    public ICollection<RogueEquipment> GetAll()
    {
        List<RogueEquipment> rogueAccounts = _EquipDAO.GetAll().ToList();

        return rogueAccounts;
    }

    public RogueItems GetByID(int Id)
    {
        throw new NotImplementedException();
    }

    public void Update(RogueEquipment item)
    {
        _EquipDAO.Update(item);
    }

    public RogueEquipment getbyItemID(int itemID)
    {
        return _EquipDAO.GetByItemID(itemID);
    }

    RogueEquipment IService<RogueEquipment>.GetByID(int Id)
    {
        throw new NotImplementedException();
    }
}