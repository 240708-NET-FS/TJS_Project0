using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;

namespace RevatureP0TimStDennis.DAO;

public class RogueMonstersDAO : IDAO<RogueMonsters>
{
    private ApplicationDbContext _context;

    public RogueMonstersDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RogueMonsters item)
    {
        throw new NotImplementedException();
    }

    public void Delete(RogueMonsters item)
    {
        throw new NotImplementedException();
    }

    public ICollection<RogueMonsters> GetAll()
    {
        throw new NotImplementedException();
    }

    public RogueMonsters GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(RogueMonsters newItem)
    {
        throw new NotImplementedException();
    }
}