using System.ComponentModel.DataAnnotations;

namespace RevatureP0TimStDennis.Entities;

public class RoguePerks
{
    [Key]
    public int PerkID {get;set;}
    public string? PerkName{get;set;}
    public string? PerkEffect{get;set;}
    public int PerkCost{get;set;}

}