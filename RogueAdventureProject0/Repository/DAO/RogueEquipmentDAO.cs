using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.DAO;

public class RogueEquipmentDAO : IDAO<RogueEquipment>
{
    private ApplicationDbContext _context;

    public RogueEquipmentDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RogueEquipment item)
    {
        _context.RogueEquipment.Add(item);
        _context.SaveChanges();
    }

    public void Delete(RogueEquipment item)
    {
        _context.RogueEquipment.Remove(item);
        _context.SaveChanges();
    }

    public ICollection<RogueEquipment> GetAll()
    {
        List<RogueEquipment> lst = _context.RogueEquipment.ToList();
        return lst;
    }

    public RogueEquipment GetByID(int ID)
    {
        RogueEquipment? item = _context.RogueEquipment.FirstOrDefault(l => l.EquipID == ID);
        if(item is not null)
        {
            return item;
        }
        throw new ItemNotFoundException();
    }

    public RogueEquipment GetByItemID(int ItemId)
    {
        RogueEquipment? equip = _context.RogueEquipment.FirstOrDefault(l => l.ItemID == ItemId);
        if(equip is not null)
        {
            return equip;
        }
        throw new ItemNotFoundException();
    }
    public void Update(RogueEquipment newItem)
    {
        RogueEquipment? item = _context.RogueEquipment.FirstOrDefault(l => l.ItemID == newItem.ItemID);

        if(item is not null)
        {
            item.DMGLow = newItem.DMGLow;
            item.DMGHigh = newItem.DMGHigh;
            item.CritChance = newItem.CritChance;
            item.PhysResist = newItem.PhysResist;
            item.FireResist = newItem.FireResist;
            item.IceResist = newItem.IceResist;
            item.WindResist = newItem.WindResist;
            item.LightResist = newItem.LightResist;
            item.DarkResist = newItem.DarkResist;
            item.Effects = newItem.Effects;
            _context.RogueEquipment.Update(item);
            _context.SaveChanges();
        }
    }
}