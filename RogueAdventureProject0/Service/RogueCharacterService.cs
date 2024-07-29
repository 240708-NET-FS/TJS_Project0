using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Service;

public class RogueCharacterService : IService<RogueCharacter>
{
    private readonly RogueCharacterDAO _chDAO;

    public RogueCharacterService(RogueCharacterDAO dao)
    {
        _chDAO = dao;
    }

    public void Create(RogueCharacter item)
    {
        _chDAO.Create(item);
    }

    public void Delete(RogueCharacter item)
    {
        throw new NotImplementedException();
    }

    public ICollection<RogueCharacter> GetAll()
    {
        throw new NotImplementedException();
    }

    public RogueCharacter GetByID(int Id)
    {
        return _chDAO.GetByID(Id);
    }

    public void Update(RogueCharacter item)
    {
        _chDAO.Update(item);
    }

    public RogueCharacter GetByName(string? cName)
    {
        return _chDAO.getByName(cName);
    }
}