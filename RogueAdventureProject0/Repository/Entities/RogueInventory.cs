using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.Identity.Client;

namespace RevatureP0TimStDennis.Entities;

public class RogueInventory
{
    [Key]
    public int InvId{get;set;}
    public int CharacterID{get;set;}
    public int ItemID{get;set;}
    public int Quantity{get;set;}

    public RogueInventory(){

    }

    public RogueInventory(int CID, int IID, int qty)
    {
        CharacterID = CID;
        ItemID = IID;
        Quantity = qty;
    }

    public override string ToString()
    {
        return $"Character ID: {CharacterID}\nItem ID: {ItemID}\nQTY: {Quantity}";
    }
}