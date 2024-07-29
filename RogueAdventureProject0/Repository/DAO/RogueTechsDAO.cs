using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;

namespace RevatureP0TimStDennis.DAO;

public class RogueTechsDAO : IDAO<RogueTechs>
{
    private ApplicationDbContext _context;

    public RogueTechsDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RogueTechs item)
    {
        throw new NotImplementedException();
    }

    public void Delete(RogueTechs item)
    {
        throw new NotImplementedException();
    }

    public ICollection<RogueTechs> GetAll()
    {
        throw new NotImplementedException();
    }

    public RogueTechs GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(RogueTechs newItem)
    {
        throw new NotImplementedException();
    }
}