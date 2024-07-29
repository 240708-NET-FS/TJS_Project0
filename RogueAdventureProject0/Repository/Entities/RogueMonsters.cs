using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace RevatureP0TimStDennis.Entities;

public class RogueMonsters
{
    [Key]
    public int MonsterID {get;set;}
    public string? MonsterName {get;set;}
    public int HealthPoints{get;set;}
    public int ManaPoints{get;set;}
    public int Strength{get;set;}
    public int Vitality{get;set;}
    public int Intelligence{get;set;}
    public int Wisdom{get;set;}
    public int EXPAward{get;set;}
    public int MoneyAward{get;set;}
    public string? ItemsRewarded{get;set;}
    public string? TechsKnown{get;set;}
    public int DMGLow {get;set;}
    public int DMGHigh{get;set;}
    public int CritChance{get;set;}
    public int PhysResist{get;set;}
    public int FireResist{get;set;}
    public int IceResist{get;set;}
    public int WindResist{get;set;}
    public int EarthResist{get;set;}
    public int LightResist{get;set;}
    public int DarkResist{get;set;}
}