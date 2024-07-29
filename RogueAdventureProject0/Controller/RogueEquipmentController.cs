using System.ComponentModel;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Controller;

public class RogueEquipmentController
{
    private RogueEquipmentService _service;

     public RogueEquipmentController(RogueEquipmentService service)
    {
        _service = service;
    }

    public void CreateEquipment(RogueEquipment item)
    {
        _service.Create(item);
    }

    public RogueItems? GetById(int activeCharacterID)
    {
        return _service.GetByID(activeCharacterID);
    }

    public RogueEquipment getByItemID(int itemID)
    {
        return _service.getbyItemID(itemID);
    }
}