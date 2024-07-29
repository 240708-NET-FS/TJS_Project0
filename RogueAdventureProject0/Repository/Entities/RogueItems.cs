using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RevatureP0TimStDennis.Entities;

public class RogueItems
{
    [Key]
    public int ItemID {get;set;}
    public string? ItemName{get;set;}
    public string? ItemType{get;set;}
    public string? ItemDescription{get;set;}
    public string? UsedEffect{get;set;}

    public int ItemCost {get;set;}
    public RogueItems()
    {
        
    }
    public RogueItems(string _name, string _type, string _descr, string _effect, int cost)
    {
        ItemName = _name;
        ItemType = _type;
        ItemDescription = _descr;
        UsedEffect = _effect;
        ItemCost = cost;
    }

    public override string ToString()
    {
        return $"{ItemName}---{ItemCost}\n{ItemType}\n{ItemDescription}";
    }
}