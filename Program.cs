using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
string dkFileName = "dk.json";
string sfFileName = "sf2.json";
List<Fighter> fighters = [];
List<Kong> kongs = [];
List<Mario> marios = [];
// Check if file exists
if (File.Exists(marioFileName))
{
    marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
    logger.Info("Characters loaded from {File}", marioFileName);
}

if (File.Exists(dkFileName))
{
    kongs = JsonSerializer.Deserialize<List<Kong>>(File.ReadAllText(dkFileName))!;
    logger.Info("Characters loaded from {File}", dkFileName);
}

if (File.Exists(sfFileName))
{
    fighters = JsonSerializer.Deserialize<List<Fighter>>(File.ReadAllText(sfFileName))!;
    logger.Info("Characters loaded from {File}", sfFileName);
}

do
{
    // display choices to user
    Console.WriteLine("1) Display Mario Characters");
    Console.WriteLine("2) Add Mario Character");
    Console.WriteLine("3) Remove Mario Character");
    Console.WriteLine("4) Display Donkey Kong Characters");
    Console.WriteLine("5) Add Donkey Kong Character");
    Console.WriteLine("6) Remove Donkey Kong Character");
    Console.WriteLine("7) Display Street Fighter Characters");
    Console.WriteLine("8) Add Street Fighter Character");
    Console.WriteLine("9) Remove Street Fighter Character");
    Console.WriteLine("0) Edit Character");
    Console.WriteLine("Enter to quit");

    // input selection
    string? choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
        // Display Mario Characters
        foreach (var c in marios)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "2")
    {
        // Add Mario Character
        // Generate unique Id
        Mario mario = new()
        {
            Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
        };
        InputCharacter(mario);
        // Add Character
        marios.Add(mario);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character added: {mario.Name}");
    }
    else if (choice == "3")
    {
        // Remove Mario Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            Mario? character = marios.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                marios.Remove(character);
                // Serialize list of characters to json
                File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
                logger.Info($"Character removed: {character.Name}");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (choice == "4")
    {
        // Display Donkey Kong Characters
        foreach (var c in kongs)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "5")
    {
        // Add Donkey Kong Character
        // Generate unique Id
        Kong kong = new()
        {
            Id = kongs.Count == 0 ? 1 : kongs.Max(c => c.Id) + 1
        };
        InputCharacter(kong);
        // Add Character
        kongs.Add(kong);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(kongs));
        logger.Info($"Character added: {kong.Name}");
    }
    else if (choice == "6")
    {
        // Remove Donkey Kong Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            Kong? character = kongs.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                kongs.Remove(character);
                // Serialize list of characters to json
                File.WriteAllText(dkFileName, JsonSerializer.Serialize(kongs));
                logger.Info($"Character removed: {character.Name}");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (choice == "7")
    {
        // Display Street Fighter Characters
        foreach (var c in fighters)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "8")
    {
        // Add Street Fighter Character
        // Generate unique Id
        Fighter fighter = new()
        {
            Id = fighters.Count == 0 ? 1 : fighters.Max(c => c.Id) + 1
        };
        InputCharacter(fighter);
        // Add Character
        fighters.Add(fighter);
        File.WriteAllText(sfFileName, JsonSerializer.Serialize(fighters));
        logger.Info($"Character added: {fighter.Name}");
    }
    else if (choice == "9")
    {
        // Remove Street Fighter Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            Fighter? character = fighters.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                fighters.Remove(character);
                // Serialize list of characters to json
                File.WriteAllText(sfFileName, JsonSerializer.Serialize(fighters));
                logger.Info($"Character removed: {character.Name}");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
    }
    else if (choice == "0")
    {
        // Edit Character
        Console.WriteLine("1) Edit Mario Character");
        Console.WriteLine("2) Edit Donkey Kong Character");
        Console.WriteLine("3) Edit Street Fighter Character");
        string? editChoice = Console.ReadLine();
        logger.Info("User choice: {Choice}", editChoice);
        if (editChoice == "1")
        {
            Console.WriteLine("Enter the Id of the Mario character to edit:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
            {
                Mario? character = marios.FirstOrDefault(c => c.Id == Id);
                if (character == null)
                {
                    logger.Error($"Character Id {Id} not found");
                }
                else
                {
                    InputCharacter(character);
                    // Serialize list of characters to json
                    File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
                    logger.Info($"Character edited: {character.Name}");
                }
            }
            else
            {
                logger.Error("Invalid Id");
            }
        }
        else if (editChoice == "2")
        {
            Console.WriteLine("Enter the Id of the Donkey Kong character to edit:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
            {
                Kong? character = kongs.FirstOrDefault(c => c.Id == Id);
                if (character == null)
                {
                    logger.Error($"Character Id {Id} not found");
                }
                else
                {
                    InputCharacter(character);
                    // Serialize list of characters to json
                    File.WriteAllText(dkFileName, JsonSerializer.Serialize(kongs));
                    logger.Info($"Character edited: {character.Name}");
                }
            }
            else
            {
                logger.Error("Invalid Id");
            }
        }
        else if (editChoice == "3")
        {
            Console.WriteLine("Enter the Id of the Street Fighter character to edit:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
            {
                Fighter? character = fighters.FirstOrDefault(c => c.Id == Id);
                if (character == null)
                {
                    logger.Error($"Character Id {Id} not found");
                }
                else
                {
                    InputCharacter(character);
                    // Serialize list of characters to json
                    File.WriteAllText(sfFileName, JsonSerializer.Serialize(fighters));
                    logger.Info($"Character edited: {character.Name}");
                }
            }
            else
            {
                logger.Error("Invalid Id");
            }
        }
        else
        {
            logger.Error("Invalid choice");
        }
    }
    else if (string.IsNullOrEmpty(choice))
    {
        break;
    }
    else
    {
        logger.Info("Invalid choice");
    }
} while (true);

logger.Info("Program ended");


static void InputCharacter(Character character)
{
    Type type = character.GetType();
    PropertyInfo[] properties = type.GetProperties();
    var props = properties.Where(p => p.Name != "Id");
    foreach (PropertyInfo prop in props)
    {
        if (prop.PropertyType == typeof(string))
        {
            Console.WriteLine($"Enter {prop.Name}:");
            prop.SetValue(character, Console.ReadLine());
        }
        else if (prop.PropertyType == typeof(List<string>))
        {
            List<string> list = [];
            do
            {
                Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
                string response = Console.ReadLine()!;
                if (string.IsNullOrEmpty(response))
                {
                    break;
                }
                list.Add(response);
            } while (true);
            prop.SetValue(character, list);
        }
    }
}