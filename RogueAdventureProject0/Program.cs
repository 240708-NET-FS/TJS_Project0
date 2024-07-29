using System.ComponentModel;
using Microsoft.IdentityModel.Tokens;
using RevatureP0TimStDennis.Controller;
using RevatureP0TimStDennis.DAO;
using RevatureP0TimStDennis.Entities;
using RevatureP0TimStDennis.Service;
using RevatureP0TimStDennis.Utility;

namespace RevatureP0TimStDennis;

public class Program
{
    public static void Main(string[] args)
    {
        using(var context = new ApplicationDbContext())
        {
            //Setup Account DAO, Service, Controller
            RogueAccountDAO acctDAO = new RogueAccountDAO(context);
            RogueAccountService acctService = new RogueAccountService(acctDAO);
            RogueAccountController acctCtrl = new RogueAccountController(acctService);
            
            //Setup Character DAO, Service, Controller
            RogueCharacterDAO charaDAO = new RogueCharacterDAO(context);
            RogueCharacterService charaService = new RogueCharacterService(charaDAO);
            RogueCharacterController charaCtrl = new RogueCharacterController(charaService);
            //Check if there are any accounts in the Database. 
            //If there are no accounts in the DB, immediately go to Creating an account.
            
            if(acctDAO.GetAll().Count == 0)
            {
                acctCtrl.CreateAccount(CreateAccount());
            }  
            State.isActive = true;

            while(State.isActive)
            {
                Console.Clear();
                if(State.currAccount == null)
                    WelcomeMessage(acctCtrl, charaCtrl);
                else
                    MainMenu(acctCtrl,charaCtrl);
            }
        }
    }
    public static RogueAccount CreateAccount()
    {
        string? userName = string.Empty;
        string? passWord = string.Empty;
        Console.WriteLine("Enter your user name(3 character minimum)");
        userName = Console.ReadLine();
        Console.WriteLine("Enter your password(3 character minimum)");
        passWord = Console.ReadLine();
        RogueAccount newAcct = new RogueAccount();
        if(userName is not null && passWord is not null)
        {
            newAcct.UserName = userName;
            newAcct.accPassword = passWord;
            newAcct.PerksGained = "none";
            newAcct.AccountMoney = 2500;
        }
        return newAcct;
    }

    public static void WelcomeMessage(RogueAccountController ctrl,RogueCharacterController chCtrl)
    {
        Console.WriteLine("Welcome to the Dungeons.");
        Console.WriteLine("1| Log In");
        Console.WriteLine("2| Register New Account");
        Console.WriteLine("3| Exit Program");
        Console.WriteLine("Please make your selection.");
        char keyInfo = Console.ReadKey(intercept: true).KeyChar;
        switch(keyInfo)
        {
            case '1':
            {
                ctrl.LogIn();
                if(State.currAccount != null)
                {
                    MainMenu(ctrl,chCtrl);
                }
                break;
            }   
            case '2':
            {
                RogueAccount newAcct = CreateAccount();
                if(newAcct is not null)
                {
                    ctrl.CreateAccount(newAcct);
                    newAcct.UserID = ctrl.GetCreatedAccountID(newAcct.UserName, newAcct.accPassword);
                    State.currAccount = newAcct;
                    MainMenu(ctrl, chCtrl);
                }
                break;
            } 
            case '3':
            default:
            {
                Console.Clear();
                State.isActive = false;
                break;
            }
        }
    }
    
    private static void MainMenu(RogueAccountController ctrl, RogueCharacterController chCtrl)
    {
        Console.Clear();
        Console.WriteLine("############## Main Menu ##############");
        bool hasAdventurer = false;
        if(State.currAccount is not null)
        {
            Console.WriteLine($"Player: {State.currAccount.UserName}\tGold: {State.currAccount.AccountMoney}");     
            if(State.currAccount.ActiveCharacterID != 0 && State.currCharacter is not null)
            {
                //Account has an active character. 
                /*
                    Active Adventurer: -Name-, the Level -level- -Class-.
                */
                Console.WriteLine("Active Adventurer: " + State.currCharacter.CName + ", the Level " +State.currCharacter.CLevel + " " + State.currCharacter.CClass);
                Console.WriteLine("1| Resume your exploration");
                hasAdventurer = true;
            }
            else
            {
                Console.WriteLine("1| Create a new Adventurer");
            }
            Console.WriteLine("2| Visit the Market");
            Console.WriteLine("3| View Current Character Details");
            Console.WriteLine("4| Equip Weapons & Armor");
            Console.WriteLine("5| Exit Program");
            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
            switch(keyInfo)
            {
                case '1':
                {
                    if(hasAdventurer)
                    {
                        //Not there yet.
                    }
                    else
                    {
                        CreateAdventurer(ctrl, chCtrl);
                    }
                    break;
                }   
                case '2':
                {
                    BuyItems(ctrl,chCtrl);
                    break;
                } 
                case '3':
                {
                    ViewDetailsOfCharacter();
                    break;
                }
                case '4':
                {
                    ViewDetailsOfCharacter(true, chCtrl);
                    break;
                }
                case '5':
                default:
                {
                    Console.Clear();
                    State.isActive = false;
                    break;
                }
                case '6':
                {
                    Admin_CreateItem();
                    break;
                }

            }
        }
    }

    //---------Character Creation--------------------
    private static void CreateAdventurer(RogueAccountController ctrl, RogueCharacterController chCtrl)
    {
        Console.Clear();
        RogueCharacter? chara = null;
        string? Name = string.Empty;
        Console.WriteLine("############## Character Creation ##############");
        Console.Write("Enter your adventurer's name: ");
        Name = Console.ReadLine();
        if(string.IsNullOrEmpty(Name))
        {
            Name = "Leeroy Jenkins";
        }
        Console.WriteLine("\nSelect your Character's Class");
        Console.WriteLine("1 | Warrior, High in HP and Strength. Low in MP and Intelligence.");
        Console.WriteLine("2 | Mage, High in MP and Intelligence. Low in HP and Strength.");
        char keyInfo = Console.ReadKey(intercept: true).KeyChar;
        switch(keyInfo)
        {
            case '1':
            {
                Console.WriteLine("Warrior Selected");
                chara = SetUpWarrior(Name);
                break;
            }
            case '2':
            {
                Console.WriteLine("Mage Selected");
                chara = SetUpMage(Name);
                break;
            }
            default:
                break;
        }
        if(chara is not null && State.currAccount is not null)
        {
            chara.CMoney = 5000;
            chCtrl.CreateCharacter(chara);
            chara = chCtrl.GetAdventurerByName(chara.CName);
            State.currAccount.ActiveCharacterID = chara.CID;
            State.currCharacter = chara;
            ctrl.UpdateAccount(State.currAccount);
            Console.WriteLine("Welcome the new adventurer\n"+ chara.ToString());
        }
        
    }

    private static RogueCharacter SetUpMage(string Name)
    {
        RogueCharacter chara = new RogueCharacter(1,Name, "Mage", 30, 50, 0, "none", 10, 12, 20, 20);
        return chara;
    }

    private static RogueCharacter SetUpWarrior(string Name)
    {
        RogueCharacter chara = new RogueCharacter(1,Name, "Warrior", 50, 25, 0, "none", 20, 20, 10, 12);
        return chara;
    }

    private static void ViewDetailsOfCharacter(bool EquipCharacter = false, RogueCharacterController? chCtrl = null)
    {
        if(State.currCharacter is not null)
        {
            Console.WriteLine(State.currCharacter.ToString());
            Console.WriteLine("############# INVENTORY #############");
            GetInventory(EquipCharacter, chCtrl);
            Console.ReadKey();
        }
    }

    private static void GetInventory(bool EquipCharacter, RogueCharacterController? chCtrl = null)
    {
        using(var context = new ApplicationDbContext())
        { 
            //Step 1, get all entries that match the active character id.
            RogueInventoryDAO invDAO = new RogueInventoryDAO(context);
            RogueInventoryService invService = new RogueInventoryService(invDAO);
            RogueInventoryController invCtrl = new RogueInventoryController(invService);
            if(State.currAccount is not null && State.currCharacter is not null)
            {
                List<RogueInventory> ids = invCtrl.GetAllAssociatedItems(State.currAccount.ActiveCharacterID);
                //Step 2, get all the proper items from the ids gathered.
                RogueItemsDAO itmDAO = new RogueItemsDAO(context);
                RogueItemsService itmService = new RogueItemsService(itmDAO);
                RogueItemsController itmCtrl = new RogueItemsController(itmService);
                RogueEquipmentDAO eqpDAO = new RogueEquipmentDAO(context);
                RogueEquipmentService eqpService = new RogueEquipmentService(eqpDAO);
                RogueEquipmentController eqpCtrl = new RogueEquipmentController(eqpService);
                if(!EquipCharacter)
                {
                    foreach(RogueInventory inv in ids)
                    {
                        string EQP = "";
                        RogueItems? item = itmCtrl.GetById(inv.ItemID);
                        if(item is not null)
                        {
                            if(item.ItemType == "Consumable")
                                Console.WriteLine(item.ItemName + " x" + inv.Quantity);
                            else
                            {
                                int equipID = eqpCtrl.getByItemID(item.ItemID).EquipID;
                                if(State.currCharacter is not null)
                                {
                                    if(State.currCharacter.MainHandID == equipID)
                                    {
                                        EQP = "{Equipped(Main Hand)}";
                                    }
                                    else if(State.currCharacter.OffHandID == equipID)
                                    {
                                        EQP = "{Equipped(Off Hand)}";
                                    }
                                    else if(State.currCharacter.HelmetID == equipID)
                                    {
                                        EQP = "{Equipped(Helmet)}";
                                    }
                                    else if(State.currCharacter.ChestID == equipID)
                                    {
                                        EQP = "{Equipped(Chest Armor)}";
                                    }
                                    else if(State.currCharacter.ArmsID == equipID)
                                    {
                                        EQP = "{Equipped(Gauntlets)}";
                                    }
                                    else if(State.currCharacter.LegsID == equipID)
                                    {
                                        EQP = "{Equipped(Leg Armor)}";
                                    }
                                    else if(State.currCharacter.BootsID == equipID)
                                    {
                                        EQP = "{Equipped(Boots)}";
                                    }
                                    else if(State.currCharacter.RingID == equipID)
                                    {
                                        EQP = "{Equipped(Ring)}";
                                    }
                                    else if(State.currCharacter.NecklaceID == equipID)
                                    {
                                        EQP = "{Equipped(Necklace)}";
                                    }
                                    Console.WriteLine(item.ItemName + EQP);
                                }
                            }
                        }
                    }
                }
                else if(EquipCharacter)
                {
                    foreach(RogueInventory inv in ids)
                    {
                        RogueItems? item = itmCtrl.GetById(inv.ItemID);
                        if(item is not null)
                        {
                            if(item.ItemType != "Consumable" && item.ItemType is not null)
                            {
                                string equipType = item.ItemType;
                                int id = -1;
                                switch(equipType)
                                {
                                    case "Main Hand":
                                    {
                                        id = State.currCharacter.MainHandID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                Console.WriteLine(equip.EquipID);
                                                State.currCharacter.MainHandID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Off Hand":
                                    {
                                        id = State.currCharacter.OffHandID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.OffHandID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Helmet":
                                    {
                                        id = State.currCharacter.HelmetID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.HelmetID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Chest Armor":
                                    {
                                        id = State.currCharacter.ChestID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                Console.WriteLine(equip.EquipID);
                                                State.currCharacter.ChestID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Gauntlets":
                                    {
                                        id = State.currCharacter.ArmsID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.ArmsID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Leg Armor":
                                    {
                                        id = State.currCharacter.LegsID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.LegsID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Boots":
                                    {
                                        id = State.currCharacter.BootsID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.BootsID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Ring":
                                    {
                                        id = State.currCharacter.RingID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.RingID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    case "Necklace":
                                    {
                                        id = State.currCharacter.NecklaceID;
                                        if(id == -1)
                                        {
                                            Console.WriteLine("Equip the " + item.ItemName + "?");
                                            char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                                            if(keyInfo == 'y' || keyInfo == 'Y')
                                            {
                                                RogueEquipment equip = eqpCtrl.getByItemID(item.ItemID);
                                                State.currCharacter.NecklaceID = equip.EquipID;
                                                if(chCtrl is not null)
                                                    chCtrl.UpdateCharacter(State.currCharacter);
                                                Console.Write("Equipped " + item.ItemName + ".");
                                            }
                                        }
                                        break;
                                    }
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static void Admin_CreateItem()
    {
        bool ItemCreated = false;
        if(State.currAccount is not null)
        {
            if(State.currAccount.UserName is not null && State.currAccount.UserName.Equals("Admin"))
            {   
                while(!ItemCreated)
                {
                    Console.WriteLine("Enter the item's name:");
                    string? itemName = Console.ReadLine();
                    if(!itemName.IsNullOrEmpty())
                    {
                        Console.WriteLine("Describe the item for me.");
                        string? itemDescription = Console.ReadLine();
                        Console.WriteLine("What type of item is this?");
                        Console.WriteLine("0| Consumable");
                        Console.WriteLine("1| Main Hand");
                        Console.WriteLine("2| Off Hand");
                        Console.WriteLine("3| Helmet");
                        Console.WriteLine("4| Chest Armor");
                        Console.WriteLine("5| Leg Armor");
                        Console.WriteLine("6| Gauntlets");
                        Console.WriteLine("7| Boots");
                        Console.WriteLine("8| Ring");
                        Console.WriteLine("9| Necklace");
                        string itemType = string.Empty;
                        char keyInfo = Console.ReadKey(intercept: true).KeyChar;
                        switch(keyInfo)
                        {
                            case '0':
                            {
                                itemType = "Consumable";
                                break;
                            }
                            case '1':
                            {
                                itemType = "Main Hand";
                                break;
                            }
                            case '2':
                            {
                                itemType = "Off Hand";
                                break;
                            }
                            case '3':
                            {
                                itemType = "Helmet";
                                break;
                            }
                            case '4':
                            {
                                itemType = "Chest Armor";
                                break;
                            }
                            case '5':
                            {
                                itemType = "Leg Armor";
                                break;
                            }
                            case '6':
                            {
                                itemType = "Gauntlets";
                                break;
                            }
                            case '7':
                            {
                                itemType = "Boots";
                                break;
                            }
                            case '8':
                            {
                                itemType = "Ring";
                                break;
                            }
                            case '9':
                            {
                                itemType = "Necklace";
                                break;
                            }
                        }
                        string? effectShortHand = string.Empty;
                        int lowDmG = 0;
                        int HiDmG = 0;
                        if(keyInfo.Equals('0'))
                        {
                            Console.WriteLine("What would be the effect short hand?");
                            effectShortHand = Console.ReadLine();          
                        }
                        else
                        {     
                            if(keyInfo.Equals('1') || keyInfo.Equals('2'))
                            {
                                Console.WriteLine("What is the low damage of this weapon?");
                                int.TryParse(Console.ReadLine(), out lowDmG);
                                Console.WriteLine("What is the high damage of this weapon?");
                                int.TryParse(Console.ReadLine(), out HiDmG);
                            }
                        }
                        int Cost = 0;
                        Console.WriteLine("How much does this item sell for?");
                        int.TryParse(Console.ReadLine(), out Cost);
                        if(itemName is not null && itemType is not null && itemDescription is not null && effectShortHand is not null)
                        {
                            RogueItems item = new RogueItems(itemName, itemType, itemDescription, effectShortHand, Cost);
                            using(var context = new ApplicationDbContext())
                            { 
                                RogueItemsDAO itmDAO = new RogueItemsDAO(context);
                                RogueItemsService itmService = new RogueItemsService(itmDAO);
                                RogueItemsController itmCtrl = new RogueItemsController(itmService);

                                itmCtrl.CreateItem(item);
                                if(!keyInfo.Equals('0'))
                                {
                                    RogueEquipmentDAO eqpDAO = new RogueEquipmentDAO(context);
                                    RogueEquipmentService eqpService = new RogueEquipmentService(eqpDAO);
                                    RogueEquipmentController eqpCtrl = new RogueEquipmentController(eqpService);

                                    RogueEquipment equip = new RogueEquipment();
                                    equip.ItemID = item.ItemID;
                                    equip.DMGHigh = HiDmG;
                                    equip.DMGLow = lowDmG;
                                    eqpCtrl.CreateEquipment(equip);
                                }
                            }
                            Console.WriteLine("Create another item?");
                            char Again = Console.ReadKey(intercept: true).KeyChar;
                            switch(Again)
                            {
                                case 'n':
                                case 'N':
                                {
                                    ItemCreated = true;
                                    break;
                                }
                                default:
                                    break;
                            }
                        }  
                    }
                }
                
            }
            else
                return;
        }
    }
    
    private static void BuyItems(RogueAccountController ctrl, RogueCharacterController chCtrl)
    {
        using(var context = new ApplicationDbContext())
        { 
            RogueItemsDAO itmDAO = new RogueItemsDAO(context);
            RogueItemsService itmService = new RogueItemsService(itmDAO);
            RogueItemsController itmCtrl = new RogueItemsController(itmService);
            bool transactionDone = false;
            List<RogueItems> canBuy = itmCtrl.GetAll().ToList();
            while(!transactionDone)
            {
                for(int i = 0; i < canBuy.Count; i++)
                {
                    Console.WriteLine("#" + i + "| " + canBuy[i].ItemName);
                }
                Console.WriteLine("Select an item to view in more detail.");
                try
                {
                    string? keyInfo = Console.ReadLine();
                    int itemToExa = 0;
                    int.TryParse(keyInfo, out itemToExa);
                    if(itemToExa <= canBuy.Count)
                    {
                        RogueItems itemToBuy = canBuy[itemToExa];
                        Console.WriteLine(itemToBuy.ToString());
                        Console.WriteLine("Do you wish to buy this? (Y or N)");
                        char ans = Console.ReadKey(intercept: true).KeyChar;
                        switch(ans)
                        {
                            case 'y':
                            case 'Y':
                            {  
                                int qty = 1;
                                if(itemToBuy.ItemType is not null && itemToBuy.ItemType.Equals("Consumable"))
                                {
                                    Console.WriteLine("How many do you wish to buy?");
                                    int.TryParse(Console.ReadLine(), out qty);
                                }
                                int finalCost = itemToBuy.ItemCost * qty;
                                Console.WriteLine("That will cost you " + finalCost + " GP");
                                if(State.currAccount is not null)
                                {
                                    if(finalCost > State.currAccount.AccountMoney)
                                        Console.WriteLine("You don't have enough gold for that!");
                                    else
                                    {
                                        Console.WriteLine("Confirm the purchase? (Y or N)");
                                        ans = Console.ReadKey(intercept: true).KeyChar;
                                        if(ans.Equals('Y') || ans.Equals('y'))
                                        {
                                            State.currAccount.AccountMoney -= finalCost;
                                            Console.WriteLine("*You bought " + qty + " " + itemToBuy.ItemName + "(s)!");
                                            RogueInventoryDAO invDAO = new RogueInventoryDAO(context);
                                            RogueInventoryService invService = new RogueInventoryService(invDAO);
                                            RogueInventoryController invCtrl = new RogueInventoryController(invService);
                                            RogueInventory inv = new RogueInventory(State.currAccount.ActiveCharacterID, itemToBuy.ItemID, qty);
                                            try{
                                                Console.WriteLine(inv.ToString());
                                                invCtrl.CreateInventory(inv);
                                                ctrl.UpdateAccount(State.currAccount);
                                            }catch(Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                                transactionDone = true;
                                                Console.ReadKey(intercept: true);
                                                Console.Clear();
                                            }
                                            
                                        }
                                        transactionDone = true;
                                        Console.ReadKey(intercept: true);
                                        Console.Clear();
                                    }
                                }
                                break;
                            }
                            default:
                                break;
                        }
                        
                    }
                      
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            
        }
    }
}