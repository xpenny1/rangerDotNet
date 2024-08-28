using System;

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

    public FileSystemEntry Select()
    {
        Keys config = IO.loadSetting(@"C:\Users\canti\Documents\C#\rangerDotNet\settings.json").Keys;
        IO.renderSelection(this);
        String key = IO.getNextKey().ToString();
        if (key == config.Next)
        {
            selectetEntry = int.Min(currentFolder.FileCount-1,selectetEntry+1);
            IO.renderSelection(this);
            return Select();
        } else if (key == config.Prev)
        {
            selectetEntry = int.Max(0,selectetEntry-1);
            IO.renderSelection(this);
            return Select();
        } else if (key == config.Down)
        {
            return currentFolder.Files[selectetEntry];
        } else if (key == config.Up)
        {
            String root = Path.GetDirectoryName(currentFolder.Self.Path);
            return new FileSystemEntry(Path.GetDirectoryName(root),root,FileType.Folder);
        }
        return currentFolder.Self;
    }

}
