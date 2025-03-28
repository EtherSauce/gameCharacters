﻿﻿using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
string dkFileName = "dk.json";
string sf2FileName = "sf2.json";
List<Mario> marios = [];
List<Dk> dks = [];
List<Sf2> sf2s = [];

// check if file exists and deserialize
if (File.Exists(marioFileName))
{
  marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
  logger.Info($"File deserialized {marioFileName}");
}

if (File.Exists(dkFileName))
{
  dks = JsonSerializer.Deserialize<List<Dk>>(File.ReadAllText(dkFileName))!;
  logger.Info($"File deserialized {dkFileName}");
}

if (File.Exists(sf2FileName))
{
  var sf2Data = JsonSerializer.Deserialize<Dictionary<string, List<Sf2>>>(File.ReadAllText(sf2FileName))!;
  sf2s = sf2Data["Characters"];
  logger.Info($"File deserialized {sf2FileName}");
}

do
{
  // display choices to user
  Console.WriteLine("1) Display Mario Characters");
  Console.WriteLine("2) Add Mario Character");
  Console.WriteLine("3) Remove Mario Character");
  Console.WriteLine("4) Edit Mario Character"); // New option
  Console.WriteLine("5) Display Donkey Kong Characters");
  Console.WriteLine("6) Add Donkey Kong Character");
  Console.WriteLine("7) Remove Donkey Kong Character");
  Console.WriteLine("8) Edit Donkey Kong Character"); // New option
  Console.WriteLine("9) Display Street Fighter II Characters");
  Console.WriteLine("10) Add Street Fighter II Character");
  Console.WriteLine("11) Remove Street Fighter II Character");
  Console.WriteLine("12) Edit Street Fighter II Character"); // New option
  Console.WriteLine("Enter to quit");

  // input selection
  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);

  if (choice == "1")
  {
    // Display Mario Characters
    foreach(var c in marios)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "2")
  {
    // Add Mario Character
    Mario mario = new()
    {
      Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
    };
    InputCharacter(mario);
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
      } else {
        marios.Remove(character);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character Id {Id} removed");
      }
    }
    else
    {
      logger.Error("Invalid Id");
    }
  }
  else if (choice == "4")
  {
    // Edit Mario Character
    Console.WriteLine("Enter the Id of the character to edit:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      int index = marios.FindIndex(c => c.Id == Id);
      if (index == -1)
      {
        logger.Error($"Character Id {Id} not found");
      }
      else
      {
        InputCharacter(marios[index]);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character Id {Id} edited");
      }
    }
    else
    {
      logger.Error("Invalid Id");
    }
  }
  else if (choice == "5")
  {
    // Display Donkey Kong Characters
    foreach (var c in dks)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "6")
  {
    // Add Donkey Kong Character
    Dk dk = new()
    {
      Id = dks.Count == 0 ? 1 : dks.Max(c => c.Id) + 1
    };
    InputCharacter(dk);
    dks.Add(dk);
    File.WriteAllText(dkFileName, JsonSerializer.Serialize(dks));
    logger.Info($"Character added: {dk.Name}");
  }
  else if (choice == "7")
  {
    // Remove Donkey Kong Character
    Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      Dk? character = dks.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      }
      else
      {
        dks.Remove(character);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(dks));
        logger.Info($"Character Id {Id} removed");
      }
    }
    else
    {
      logger.Error("Invalid Id");
    }
  }
  else if (choice == "8")
  {
    // Edit Donkey Kong Character
    Console.WriteLine("Enter the Id of the character to edit:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      int index = dks.FindIndex(c => c.Id == Id);
      if (index == -1)
      {
        logger.Error($"Character Id {Id} not found");
      }
      else
      {
        InputCharacter(dks[index]);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(dks));
        logger.Info($"Character Id {Id} edited");
      }
    }
    else
    {
      logger.Error("Invalid Id");
    }
  }
  else if (choice == "9")
  {
    // Display Street Fighter II Characters
    foreach (var c in sf2s)
    {
      Console.WriteLine(c.Display());
    }
  }
  else if (choice == "10")
  {
    // Add Street Fighter II Character
    Sf2 sf2 = new()
    {
      Id = sf2s.Count == 0 ? 1 : sf2s.Max(c => c.Id) + 1
    };
    InputCharacter(sf2);
    sf2s.Add(sf2);
    File.WriteAllText(sf2FileName, JsonSerializer.Serialize(new { Characters = sf2s }));
    logger.Info($"Character added: {sf2.Name}");
  }
  else if (choice == "11")
  {
    // Remove Street Fighter II Character
    Console.WriteLine("Enter the Id of the character to remove:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      Sf2? character = sf2s.FirstOrDefault(c => c.Id == Id);
      if (character == null)
      {
        logger.Error($"Character Id {Id} not found");
      }
      else
      {
        sf2s.Remove(character);
        File.WriteAllText(sf2FileName, JsonSerializer.Serialize(new { Characters = sf2s }));
        logger.Info($"Character Id {Id} removed");
      }
    }
    else
    {
      logger.Error("Invalid Id");
    }
  }
  else if (choice == "12")
  {
    // Edit Street Fighter II Character
    Console.WriteLine("Enter the Id of the character to edit:");
    if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
    {
      int index = sf2s.FindIndex(c => c.Id == Id);
      if (index == -1)
      {
        logger.Error($"Character Id {Id} not found");
      }
      else
      {
        InputCharacter(sf2s[index]);
        File.WriteAllText(sf2FileName, JsonSerializer.Serialize(new { Characters = sf2s }));
        logger.Info($"Character Id {Id} edited");
      }
    }
    else
    {
      logger.Error("Invalid Id");
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
    } else if (prop.PropertyType == typeof(List<string>)) {
      List<string> list = [];
      do {
        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
        string response = Console.ReadLine()!;
        if (string.IsNullOrEmpty(response)){
          break;
        }
        list.Add(response);
      } while (true);
      prop.SetValue(character, list);
    }
  }
}