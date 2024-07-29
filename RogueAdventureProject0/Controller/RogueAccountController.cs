using System.ComponentModel;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Controller;

public class RogueAccountController
{
    private RogueAccountService _service;

    public RogueAccountController(RogueAccountService service)
    {
        _service = service;
    }

    public void LogIn()
    {
        Console.Write("User Name: ");
        string? userName = Console.ReadLine();
        Console.Write("Password: ");
        string? passWord = string.Empty;
        ConsoleKey key;
        do //While we're entering our password.
        {
            var keyInfo = Console.ReadKey(intercept: true); // Intercept the key given, and do not display it.
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && passWord.Length > 0)
            {
                //If we hit backspace, remove the previous character.
                Console.Write("\b \b");
                passWord = passWord[0..^1]; 
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                //Otherwise Write * to the console, and add the character pressed to passWord.
                Console.Write("*");
                passWord += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine("Logging in...");
        State.currAccount = AttemptLogIn(userName, passWord);
        Console.ReadLine();
    }

    public RogueAccount? AttemptLogIn(string? userName, string passWord)
    {
        RogueAccount? acct = null;
        try
        {
            //For some reason, the program complains that username & password might be null, despite all context stating otherwise...
            //...without the "is not null" keyphrase.
            if((userName is not null && userName.Length > 3) && (passWord is not null && passWord.Length > 3))
            {
                try
                {
                    acct = _service.LogIn(userName, passWord);
                }
                catch(PasswordIncorrect){}
                catch(AccountNotFoundException){}
                if(acct is not null)
                {
                    Console.WriteLine("Logged in. Welcome back to the dungeons, " + acct.UserName);
                }     
            }
            else
                throw new InvalidInputException();
        }
        catch (InvalidInputException){}
        catch(PasswordIncorrect){}
        catch(AccountNotFoundException) {}
        return acct;
    }

    public void CreateAccount(RogueAccount acct)
    {
        _service.Create(acct);
    }

    public RogueAccount GetRogueAccountByID(int id)
    {
        return _service.GetByID(id);
    }

    public void DeleteAccount(RogueAccount acct)
    {
        _service.Delete(acct);
    }

    public void UpdateAccount(RogueAccount acct)
    {
        _service.Update(acct);
    }

    public int GetCreatedAccountID(string userName, string password)
    {
        return _service.GetCreatedAccountID(userName, password);
    }
}