using Xunit;
using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Controller;
using Microsoft.SqlServer.Server;
using RevatureP0TimStDennis.Utility.Exceptions;
using Xunit.Priority;

namespace RevatureP0TimStDennis.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class AccountTests()
{
    [Fact]
    public void DBSetLinkSuccessful()
    {
        bool result = false;
        using(var context = new ApplicationDbContext())
        {
            if(context.RogueAccount != null)
            {
                if(context.RogueCharacter != null)
                {
                    if(context.RogueEquipment != null)
                    {
                        if(context.RogueInventory != null)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        Assert.True(result, "All DB sets are valid and ready.");
    }

    [Fact]
    public void AccountSetupSuccessful()
    {
        bool result = false;
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            if(acctDAO != null)
            {
                RogueAccountService acctService = new RogueAccountService(acctDAO);
                if(acctService != null)
                {
                    RogueAccountController acctCtrl = new RogueAccountController(acctService);
                    if(acctCtrl != null)
                    {
                        result = true;
                    }
                }
            }      
        }
        Assert.True(result, "Account DAO, Service and Controller created successfully.");
    }

    [Theory, Priority(-1)]
    [InlineData("TestUser01","TestPassWord")]
    public void AccountCreationSuccessful(string userName, string password)
    {
        bool result = false;
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            RogueAccountService acctService = new RogueAccountService(acctDAO);
            RogueAccountController acctCtrl = new RogueAccountController(acctService);
            RogueAccount? testAcct = new RogueAccount();
            testAcct.UserName = userName;
            testAcct.accPassword = password;
            testAcct.PerksGained = "none";
            acctCtrl.CreateAccount(testAcct);
            testAcct = null;
            testAcct = acctDAO.GetByUNAndPW(userName, password);
            if(testAcct != null)
            {
                //Entry was found.
                result = true;
            }
            Assert.True(result, "Successfully created an account.");
        }   
    }

    [Theory, Priority(1)]
    [InlineData("TestUser01","TestPassWord")]
    public void LogIn_Successful(string userName, string password)
    {
        bool result = false;
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            RogueAccountService acctService = new RogueAccountService(acctDAO);
            RogueAccountController acctCtrl = new RogueAccountController(acctService);
            RogueAccount? testAcct = acctCtrl.AttemptLogIn(userName, password);
            if(testAcct is not null)
            {
                result = true;
            }
        }
        Assert.True(result, "Log in was successful!");
    }

    [Theory, Priority(2)]
    [InlineData("TestUser1250","TestPassWord")]
    public void LogIn_CantFindUser(string userName, string password)
    {
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            Action testCode = () => 
                { 
                    try
                    {
                        RogueAccount? testAcct = acctDAO.GetByUNAndPW(userName, password);
                    }catch(AccountNotFoundException)
                    {
                        throw;
                    }
                };
            Assert.Throws<AccountNotFoundException>(testCode);
        } 
    }

    [Theory,Priority(3)]
    [InlineData("TestUser01","TestPAssWoRd1")]
    public void LogIn_IncorrectPW(string userName, string password)
    {
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            Action testCode = () => 
                { 
                    try
                    {
                        RogueAccount? testAcct = acctDAO.GetByUNAndPW(userName, password);
                    }catch(PasswordIncorrect)
                    {
                        throw;
                    }
                };
            Assert.Throws<PasswordIncorrect>(testCode);
        } 
    }

    [Theory,Priority(4)]
    [InlineData("TestUser01","TestPassWord")]
    public void Account_DeleteSuccessful(string userName, string password)
    {
        using(var context = new ApplicationDbContext())
        {
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            RogueAccountService acctService = new RogueAccountService(acctDAO);
            RogueAccountController acctCtrl = new RogueAccountController(acctService);
            RogueAccount? testAcct = acctDAO.GetByUNAndPW(userName, password);
            if(testAcct != null)
            {
                acctCtrl.DeleteAccount(testAcct);
                Action testCode = () => 
                { 
                    try
                    {
                        RogueAccount? testAcct = acctDAO.GetByUNAndPW(userName, password);
                    }catch(AccountNotFoundException)
                    {
                        throw;
                    }
                };
                Assert.Throws<AccountNotFoundException>(testCode);
            }
        } 
    }
}