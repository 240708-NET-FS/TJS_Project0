using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Service;

public class RogueAccountService : IService<RogueAccount>
{
    private readonly RogueAccountDAO _acctDAO;

    public RogueAccountService(RogueAccountDAO dao)
    {
        _acctDAO = dao;
    }

    public RogueAccount? LogIn(string username, string password)
    {
        if(username.Length <= 3 || password.Length <= 3)
        {
            throw new InvalidInputException("Invalid Input");
        }
        RogueAccount? attempt = null;
        try
        {
             attempt = _acctDAO.GetByUNAndPW(username,password);
        }
        catch(PasswordIncorrect){}
        catch(AccountNotFoundException){}
        catch(LoginException){}

        if(attempt is not null)
        {
            return attempt;
        }
        return null;
    }

    public void Create(RogueAccount item)
    {
        _acctDAO.Create(item);
    }

    public void Delete(RogueAccount item)
    {
        _acctDAO.Delete(item);
    }

    public ICollection<RogueAccount> GetAll()
    {
        List<RogueAccount> rogueAccounts = _acctDAO.GetAll().ToList();

        return rogueAccounts;
    }

    public RogueAccount GetByID(int Id)
    {      
        throw new NotImplementedException();
    }

    public void Update(RogueAccount item)
    {
        _acctDAO.Update(item);
    }

    public int GetCreatedAccountID(string userName, string password)
    {
        RogueAccount? acct = _acctDAO.GetByUNAndPW(userName, password);
        if(acct is not null)
        {
            return acct.UserID;
        }
        return 0;
    }
}