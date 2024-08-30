using System;
using System.ComponentModel;
using System.Numerics;

namespace rangerDotNet;

public static class IO
{
    public static ConsoleKey getNextKey()
    {
        return Console.ReadKey(true).Key;
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
    public static Settings loadSetting(String configFilePath)
    {
        Settings config = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(configFilePath));
        return config;
    }
}
