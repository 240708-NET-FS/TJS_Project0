using System.ComponentModel;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Controller;

public class RogueItemsController
{
    private RogueItemsService _service;

     public RogueItemsController(RogueItemsService service)
    {
        _service = service;
    }

    public void CreateItem(RogueItems item)
    {
        _service.Create(item);
    }

    public RogueItems? GetById(int activeCharacterID)
    {
        return _service.GetByID(activeCharacterID);
    }

    public ICollection<RogueItems> GetAll()
    {
        return _service.GetAll();
    }
}