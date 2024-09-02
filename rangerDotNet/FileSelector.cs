using System;
using System.Diagnostics;

namespace rangerDotNet;

public class FileSelector
{
    public Folder currentFolder {get;}
    public int selectetEntry {get;set;}

    public FileSelector()
    {
        selectetEntry = 0;
        String dirPath = Directory.GetCurrentDirectory();
        String dirName = Path.GetFileName(dirPath);
        String[] entryPaths = Directory.GetFileSystemEntries(dirPath);
        String[] filePaths = Directory.GetFiles(dirPath);
        FileSystemEntry[] files = new FileSystemEntry[entryPaths.Length];
        for (int i = 0; i < entryPaths.Length; i++)
        {
            String path = entryPaths[i];
            FileAttributes entryAttributes = File.GetAttributes(path);
            if ((entryAttributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                files[i] = new FileSystemEntry(Path.GetFileName(path), path, FileType.Folder);
            } else
            {
                files[i] = new FileSystemEntry(Path.GetFileName(path), path, FileType.File);
            }
        }
        currentFolder = new Folder(new FileSystemEntry(dirName,dirPath,FileType.Folder), files, entryPaths.Length);
    }

    public FileSystemEntry? Select()
    {
        Settings config = IO.loadSetting(@"C:\Users\canti\Documents\C#\rangerDotNet\settings.json");
        IO.renderSelectionDynamicallyCentered(this);
        String key = IO.getNextKey().ToString();
        if (config.Next.Any(k => k.KeyString == key))
        {
            selectetEntry = int.Min(currentFolder.FileCount-1,selectetEntry+1);
            IO.renderSelectionDynamicallyCentered(this);
            return Select();
        } else if (config.Prev.Any(k => k.KeyString == key))
        {
            selectetEntry = int.Max(0,selectetEntry-1);
            IO.renderSelectionDynamicallyCentered(this);
            return Select();
        } else if (config.Down.Any(k => k.KeyString == key))
        {
            return currentFolder.Files[selectetEntry];
        } else if (config.Up.Any(k => k.KeyString == key))
        {
            String root = Path.GetDirectoryName(currentFolder.Self.Path);
            return new FileSystemEntry(Path.GetDirectoryName(root),root,FileType.Folder);
        } else if (config.Quit.Any(k => k.KeyString == key))
        {
            return null;
        } else if (config.Command.Any(k => k.KeyString == key))
        {
            Console.SetCursorPosition(0,Console.BufferHeight-1);
            String comm = Console.ReadLine();
            comm = comm.Replace("%f",currentFolder.Files[selectetEntry].Path);
            Process cmd = new Process();
            cmd.StartInfo.FileName = config.Shell;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(comm);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            Console.ReadLine();
        }
        return currentFolder.Self;
    }

}
