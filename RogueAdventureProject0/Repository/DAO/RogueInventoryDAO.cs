using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;

namespace RevatureP0TimStDennis.DAO;

public class RogueInventoryDAO : IDAO<RogueInventory>
{
    private ApplicationDbContext _context;

    public RogueInventoryDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RogueInventory item)
    {
        _context.RogueInventory.Add(item);
        _context.SaveChanges();
    }

    public void Delete(RogueInventory item)
    {
        _context.RogueInventory.Remove(item);
        _context.SaveChanges();
    }

    public ICollection<RogueInventory> GetAll()
    {
        return _context.RogueInventory.ToList();
    }

    public ICollection<RogueInventory> GetAllByCID(int ID)
    {
        List<RogueInventory> lst = _context.RogueInventory.ToList().FindAll(ri => ri.CharacterID == ID);
        return lst;
    }

    public RogueInventory GetByID(int ID)
    {
        RogueInventory? inv = _context.RogueInventory.FirstOrDefault(l => l.InvId == ID);
        if(inv is not null)
            return inv;
        throw new Exception();
    }

    public void Update(RogueInventory newItem)
    {
        RogueInventory? inv = _context.RogueInventory.FirstOrDefault(
                ri => ri.ItemID == newItem.ItemID &&
                ri.CharacterID == newItem.CharacterID
        );
        if(inv is not null)
        {
            inv.CharacterID = newItem.CharacterID;
            inv.ItemID = newItem.ItemID;
            inv.Quantity = newItem.Quantity;
            _context.RogueInventory.Update(inv);
            _context.SaveChanges();
        }
    }
}