using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;

namespace RevatureP0TimStDennis.DAO;

public class RoguePerksDAO : IDAO<RoguePerks>
{
    private ApplicationDbContext _context;

    public RoguePerksDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RoguePerks item)
    {
        throw new NotImplementedException();
    }

    public void Delete(RoguePerks item)
    {
        throw new NotImplementedException();
    }

    public ICollection<RoguePerks> GetAll()
    {
        throw new NotImplementedException();
    }

    public RoguePerks GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(RoguePerks newItem)
    {
        throw new NotImplementedException();
    }
}