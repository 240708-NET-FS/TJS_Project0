using RevatureP0TimStDennis.Entities;

namespace RevatureP0TimStDennis.Utility;

public static class State
{
    public static bool isActive {get;set;}

    public static RogueAccount? currAccount {get;set;}

    public static RogueCharacter? currCharacter {get;set;}
}