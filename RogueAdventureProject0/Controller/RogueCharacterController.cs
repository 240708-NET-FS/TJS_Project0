using System.ComponentModel;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;
using RevatureP0TimStDennis.Utility.Exceptions;

namespace RevatureP0TimStDennis.Controller;

public class RogueCharacterController
{
    private RogueCharacterService _service;

     public RogueCharacterController(RogueCharacterService service)
    {
        _service = service;
    }

    public void CreateCharacter(RogueCharacter item)
    {
        _service.Create(item);
    }

    public RogueCharacter GetAdventurerByName(string? cName)
    {
        return _service.GetByName(cName);
    }

    public RogueCharacter? GetById(int activeCharacterID)
    {
        return _service.GetByID(activeCharacterID);
    }

    public void UpdateCharacter(RogueCharacter currCharacter)
    {
        _service.Update(currCharacter);
    }
}