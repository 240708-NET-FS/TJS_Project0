using System.ComponentModel.DataAnnotations;

namespace RevatureP0TimStDennis.Entities;

public class RogueCharacter
{
    [Key]
    public int CID {get;set;}
    public string? CName{get;set;}
    public int CLevel {get;set;}
    public string? CClass{get;set;}
    public int HealthPoints{get;set;}
    public int ManaPoints{get;set;}
    public int Strength {get;set;}
    public int Vitality {get;set;}
    public int Intelligence {get;set;}
    public int Wisdom{get;set;}
    public int CurrentExperience{get;set;}
    public int ToNextLevel{get;set;}
    public int CMoney{get;set;}
    public string? TechsKnown{get;set;}
    public int MainHandID{get;set;}
    public int OffHandID{get;set;}
    public int HelmetID{get;set;}
    public int ChestID{get;set;}
    public int LegsID{get;set;}
    public int ArmsID{get;set;}
    public int BootsID{get;set;}
    public int RingID{get;set;}
    public int NecklaceID{get;set;}
    
    public RogueCharacter()
    {

    }

    public RogueCharacter(int Level, string Name, string Class, int Health, int Mana, int exp, string techs, int Str, int Vit, int Int, int Wis)
    {
        CLevel = Level;
        CName = Name;
        CClass = Class;
        HealthPoints = Health;
        ManaPoints = Mana;
        CurrentExperience = exp;
        ToNextLevel = 1000;
        TechsKnown = techs;
        Strength = Str;
        Vitality = Vit;
        Intelligence = Int;
        Wisdom = Wis;
        MainHandID = -1;
        OffHandID = -1;
        HelmetID =-1;
        ChestID = -1;
        ArmsID = -1;
        LegsID =-1;
        BootsID = -1;
        RingID = -1;
        NecklaceID = -1;
    }
    public override string ToString()
    {
        return $"{CName} the Level {CLevel} {CClass}\nHealth Points: {HealthPoints}\tMana Points: {ManaPoints}\nStrength: {Strength}\tVitality: {Vitality}\nIntelligence: {Intelligence}\tWisdom: {Wisdom}\nExperience Points: {CurrentExperience}\\{ToNextLevel}\nGold: {CMoney}";
    }

}