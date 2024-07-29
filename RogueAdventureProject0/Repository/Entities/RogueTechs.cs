using System.ComponentModel.DataAnnotations;

namespace RevatureP0TimStDennis.Entities;

public class RogueTechs
{
    [Key]
    public int TechID{get;set;}
    public string? TechName{get;set;}
    public string? TechEffect{get;set;}
    public int TechCost{get;set;}
}