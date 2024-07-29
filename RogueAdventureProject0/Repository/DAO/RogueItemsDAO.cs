using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.DAO;

public class RogueItemsDAO : IDAO<RogueItems>
{

    private ApplicationDbContext _context;

    public RogueItemsDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }
    public void Create(RogueItems item)
    {
        _context.rogueItems.Add(item);
        _context.SaveChanges();
    }

    public void Delete(RogueItems item)
    {
        _context.rogueItems.Remove(item);
        _context.SaveChanges();
    }

    public ICollection<RogueItems> GetAll()
    {
        List<RogueItems> lst = _context.rogueItems.ToList();
        return lst;
    }

    public RogueItems GetByID(int ID)
    {
        RogueItems? item = _context.rogueItems.FirstOrDefault(l => l.ItemID == ID);
        if(item is not null)
        {
            return item;
        }
        throw new ItemNotFoundException();
    }

    public void Update(RogueItems newItem)
    {
        RogueItems? item = _context.rogueItems.FirstOrDefault(l => l.ItemID == newItem.ItemID);

        if(item is not null)
        {
            item.ItemName = newItem.ItemName;
            item.ItemType = newItem.ItemType;
            item.ItemDescription = newItem.ItemDescription;
            item.UsedEffect = newItem.UsedEffect;
            _context.rogueItems.Update(item);
            _context.SaveChanges();
        }

    }
}