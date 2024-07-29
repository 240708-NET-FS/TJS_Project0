using System.ComponentModel.DataAnnotations;

namespace RevatureP0TimStDennis.Entities;

public class RogueEquipment
{
    [Key]
    public int EquipID {get;set;}
    public int ItemID{get;set;}
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
    public string? Effects{get;set;}

    public RogueItems? rogueItems;
}