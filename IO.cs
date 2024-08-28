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
    public static void renderSelection(FileSelector selector)
    {
        Console.Clear();
        int conWidth  = Console.WindowWidth;
        int conHeight = Console.WindowHeight;

        String headline    = selector.currentFolder.Self.Name.Substring(0,int.Min(selector.currentFolder.Self.Name.Length,conWidth));
        int headlineSpaces = int.Max((conWidth - headline.Length)/2,0);
        Console.SetCursorPosition(headlineSpaces,0);
        Console.Write(headline);

        int showableEntries = int.Min(selector.currentFolder.FileCount,conHeight-2)-1;
        int firstEntry      = int.Max(0,selector.selectetEntry-(conHeight-2)/2); 
        var indexedEntries = selector.currentFolder.Files.Select((entry,index) => new {entry,index});
        var entries = 
            (
                from ent in indexedEntries
                where ent.index >= firstEntry
                where ent.index <= firstEntry+showableEntries
                select new {entry=ent.entry,index=ent.index-firstEntry}
            );
        int maxEntryLength =
            (
                from ent in entries
                select ent.entry.Name.Length
            ).Max();
        
        if (maxEntryLength+3 > conWidth)
        {
            foreach (var ent in entries)
            {
                if (ent.index == selector.selectetEntry)
                {
                    Console.SetCursorPosition(0,ent.index+2);
                    Console.Write($"=> {ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                } else
                {
                    Console.SetCursorPosition(2,ent.index+2);
                    Console.Write($"{ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                }
            }
        } else
        {
            foreach (var ent in entries)
            {
                if (ent.index == selector.selectetEntry)
                {
                    Console.SetCursorPosition(0,ent.index+2);
                    Console.Write($"=> {ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                } else
                {
                    Console.SetCursorPosition(2,ent.index+2);
                    Console.Write($"{ent.entry.Name.Substring(0,int.Min(conWidth-3,ent.entry.Name.Length))}");
                }
            }

        }
    }
//    public static void renderSelection(FileSelector selection)
//    {
//        int headlineSpaces = (Console.WindowWidth - selection.currentFolder.Self.Name.Length) / 2;
//        int maxEntryLength = ( from file in selection.currentFolder.Files
//                               select file.Name.Length
//                             ).Max() ; 
//        int entrySpaces = (Console.WindowWidth - maxEntryLength) / 2;                             
//        if (selection.currentFolder.FileCount > Console.WindowHeight-4 || Console.WindowWidth < int.Max(selection.currentFolder.Self.Name.Length,maxEntryLength))
//        {
//            Console.WriteLine($"Terminal is to small!!!");
//            return;
//        }
//        Console.Clear();
//        Console.SetCursorPosition(headlineSpaces,0);
//        Console.Write(selection.currentFolder.Self.Name);
//        for (int i = 0; i < selection.currentFolder.FileCount; i++)
//        {
//            if (i == selection.selectetEntry)
//            {
//                Console.SetCursorPosition(entrySpaces-2,i+2);
//                Console.Write($"=> ({i}) {selection.currentFolder.Files[i].Name}");
//            } else
//            {
//                Console.SetCursorPosition(entrySpaces,i+2);
//                Console.WriteLine($"({i}) {selection.currentFolder.Files[i].Name}");
//            }
//        }
//        Console.SetCursorPosition(0,Console.WindowHeight-1);
//    }
    public static Settings loadSetting(String configFilePath)
    {
        Settings config = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(configFilePath));
        return config;
    }
}
