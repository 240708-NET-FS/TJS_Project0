using System.ComponentModel;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Controller;

public class RogueInventoryController
{
    private RogueInventoryService _service;

     public RogueInventoryController(RogueInventoryService service)
    {
        _service = service;
    }

    public void CreateInventory(RogueInventory item)
    {
        _service.Create(item);
    }

    public List<RogueInventory> GetAllAssociatedItems(int activeCharacterID)
    {
        return _service.GetAllByCID(activeCharacterID).ToList();
    }
}