using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace RevatureP0TimStDennis.DAO;

public class RogueAccountDAO : IDAO<RogueAccount>
{

    private ApplicationDbContext _context;

    public RogueAccountDAO(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    public void Create(RogueAccount acct)
    {
        _context.RogueAccount.Add(acct);
        _context.SaveChanges();
    }

    public void Delete(RogueAccount acct)
    {
        _context.RogueAccount.Remove(acct);
        _context.SaveChanges();
    }

    public ICollection<RogueAccount> GetAll()
    {
        List<RogueAccount> accts = _context.RogueAccount.ToList();

        return accts;
    }

    public RogueAccount GetByID(int ID)
    {
        RogueAccount? account = _context.RogueAccount.FirstOrDefault(l => l.UserID == ID);
        if(account is not null)
        {
            return account;
        }
        throw new AccountNotFoundException();
    }

    public void Update(RogueAccount newacct)
    {
        RogueAccount? orig = _context.RogueAccount.FirstOrDefault(l => l.UserID == newacct.UserID);

        if(orig is not null)
        {
            orig.AccountMoney = newacct.AccountMoney;
            orig.accPassword = newacct.accPassword;
            orig.ActiveCharacterID = newacct.ActiveCharacterID;
            orig.HighestLevel = newacct.HighestLevel;
            orig.PerksGained = newacct.PerksGained;
            orig.UserName = newacct.UserName;
            _context.RogueAccount.Update(orig);
            _context.SaveChanges();
        }
        else
            throw new AccountNotFoundException();
    }

    public RogueAccount? GetByUNAndPW(string username, string password) 
    {
        try
        {
            RogueAccount? logIn = _context.RogueAccount.FirstOrDefault(l => l.UserName == username);
            if(logIn is not null)
            {
                if(logIn.accPassword is not null && logIn.accPassword.Equals(password))
                    return logIn;
                else
                    throw new PasswordIncorrect();
            }  
            else if(logIn is null)
                throw new AccountNotFoundException();
        }
        catch(AccountNotFoundException){throw;}
        catch(PasswordIncorrect){throw;}
        return null;
        
    }
}