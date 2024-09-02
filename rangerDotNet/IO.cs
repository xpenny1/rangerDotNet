using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace rangerDotNet;

public static class IO
{
    public static char getNextKey()
    {
        return Console.ReadKey(true).KeyChar;
    }
    public static void renderSelectionDynamicallyCentered(FileSelector selector)
    {
        Console.Clear();
        int conWidth  = Console.WindowWidth;
        int conHeight = Console.WindowHeight;

        String headline    = selector.currentFolder.Self.Name.Substring(0,int.Min(selector.currentFolder.Self.Name.Length,conWidth));
        int headlineSpaces = (conWidth - headline.Length)/2;
        Console.SetCursorPosition(headlineSpaces,0);
        Console.Write(headline);

        int showableEntries = int.Min(selector.currentFolder.FileCount,conHeight-3);
        int firstEntry      = int.Min(int.Max(0,selector.selectetEntry - showableEntries/2),selector.currentFolder.FileCount-showableEntries); 
        var indexedEntries = selector.currentFolder.Files.Select((entry,index) => new {entry,index});
        var indexedShownEntries = 
            (
                from ent in indexedEntries
                where ent.index >= firstEntry
                where ent.index < firstEntry+showableEntries
                select new {entry=ent.entry,index=ent.index,showIndex=ent.index-firstEntry}
            );
        int maxEntryLength =
            (
                from ent in indexedShownEntries
                select ent.entry.Name.Length
            ).Max();
        
        if (maxEntryLength+3 > conWidth)
        {
            foreach (var ent in indexedShownEntries)
            {
                if (ent.index == selector.selectetEntry)
                {
                    Console.SetCursorPosition(0,ent.showIndex+2);
                    Console.Write($"=> {ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                } else
                {
                    Console.SetCursorPosition(2,ent.showIndex+2);
                    Console.Write($"{ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                }
            }
        } else
        {
            int entrySpaces = (conWidth-(maxEntryLength+3))/2;
            foreach (var ent in indexedShownEntries)
            {
                if (ent.index == selector.selectetEntry)
                {
                    Console.SetCursorPosition(entrySpaces,ent.showIndex+2);
                    Console.Write($"=> {ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                } else
                {
                    Console.SetCursorPosition(entrySpaces+2,ent.showIndex+2);
                    Console.Write($"{ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                }
            }

        }
    }
    //public static SettingsRaw loadSetting(String configFilePath)
    //{
    //    SettingsRaw config = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingsRaw>(System.IO.File.ReadAllText(configFilePath));
    //    return config;
    //}

    public static Key readKey(JsonNode node)
    {
        if (node.GetType().Equals(typeof(JsonObject)))
        {
            return new Key(node["Name"]!.ToString(),ConsoleModifiers.None);
        } else 
        {
            return new Key(node.ToString());
        }
    }
    public static Key[] readKeysArray(JsonNode nodes)
    {
        if (nodes.GetType().Equals(typeof(JsonArray)))
        {
            IEnumerable<Key> arr = from node in nodes.AsArray()
                                   select readKey(node);
            return arr.ToArray();
        } else
        {
            return [readKey(nodes)];
        }
    }

    //using NUnit.Test;
    //[Test]
    //public static void testReadKeys()
    //{
    //    JsonNode node = JsonNode.Parse("""
    //        {
    //            "Name": "Down",
    //            "Modifiers": None
    //        }
    //    """);
    //    Key key = readKey(node);
    //    Assert.AreEqual(key,new Key("Down",KeyModifiers.None));
    //}
    
    public static Settings loadSetting(String configFilePath)
    {
        string file = System.IO.File.ReadAllText(configFilePath);
        JsonNode settings = JsonNode.Parse(file)!;
        JsonNode keys = settings["Keys"]!;
        JsonNode os = settings["Windows"]!;
        Dictionary<String,String> commandReplacements = new Dictionary<string, string>();
        return new Settings(
            readKeysArray(keys["Prev"]!),
            readKeysArray(keys["Next"]!),
            readKeysArray(keys["Up"]!),
            readKeysArray(keys["Up"]!),
            readKeysArray(keys["Down"]!),
            readKeysArray(keys["Command"]!),
            os["Shell"]!.ToString(),
            os["Terminal"]!.ToString(),
            commandReplacements,
            true,
            true,
            Style.Centered
            );
    }
}
