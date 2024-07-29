using RevatureP0TimStDennis.Entities;
using Microsoft.EntityFrameworkCore;
using RevatureP0TimStDennis.Utility.Exceptions;
namespace RevatureP0TimStDennis.DAO;

public class RogueCharacterDAO : IDAO<RogueCharacter>
{

    private ApplicationDbContext _context;

    public RogueCharacterDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }
    public void Create(RogueCharacter chara)
    {
        _context.RogueCharacter.Add(chara);
        _context.SaveChanges();
    }

    public void Delete(RogueCharacter item)
    {
        throw new NotImplementedException();
    }

    public ICollection<RogueCharacter> GetAll()
    {
        throw new NotImplementedException();
    }

    public RogueCharacter GetByID(int ID)
    {
        RogueCharacter? character = _context.RogueCharacter.FirstOrDefault(l => l.CID == ID);
        if(character is not null)
        {
            return character;
        }
        throw new CharacterNotFoundException();
    }

    public void Update(RogueCharacter newItem)
    {
        RogueCharacter? orig = _context.RogueCharacter.FirstOrDefault(l => l.CID == newItem.CID);
        if(orig is not null)
        {
            orig.ArmsID = newItem.ArmsID;
            orig.BootsID = newItem.BootsID;
            orig.CClass = newItem.CClass;
            orig.ChestID = newItem.ChestID;
            orig.CLevel = newItem.CLevel;
            orig.CMoney = newItem.CMoney;
            orig.CName = newItem.CName;
            orig.CurrentExperience = newItem.CurrentExperience;
            orig.HealthPoints = newItem.HealthPoints;
            orig.ManaPoints = newItem.ManaPoints;
            orig.HelmetID = newItem.HelmetID;
            orig.Intelligence = newItem.Intelligence;
            orig.LegsID = newItem.LegsID;
            orig.MainHandID = newItem.MainHandID;
            orig.NecklaceID = newItem.NecklaceID;
            orig.OffHandID = newItem.OffHandID;
            orig.RingID = newItem.RingID;
            orig.Strength = newItem.Strength;
            orig.ToNextLevel = newItem.ToNextLevel;
            orig.TechsKnown = newItem.TechsKnown;
            orig.Vitality = newItem.Vitality;
            orig.Wisdom = newItem.Wisdom;
            _context.RogueCharacter.Update(orig);
            _context.SaveChanges();
        }
    }

    public RogueCharacter getByName(string? cName)
    {
        RogueCharacter? character = _context.RogueCharacter.FirstOrDefault(l => l.CName == cName);
        if(character is not null)
        {
            return character;
        }
        throw new CharacterNotFoundException();
    }
}