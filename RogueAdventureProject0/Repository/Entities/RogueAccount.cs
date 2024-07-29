using System.ComponentModel.DataAnnotations;

namespace RevatureP0TimStDennis.Entities;

public class RogueAccount
{
    [Key]
    public int UserID {get;set;}
    public string? UserName{get;set;}
    public string? accPassword{get;set;}
    public int HighestLevel{get;set;}
    public string? PerksGained{get;set;}
    public int AccountMoney{get;set;}
    public int ActiveCharacterID{get;set;}

    public override string ToString()
    {
        return $"Hello {UserName}.\nYour Highest Level achieved is Level {HighestLevel}.\nYou currently have {AccountMoney} gold to spend on Perks for your next run.";
    }
}