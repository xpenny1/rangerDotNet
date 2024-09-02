namespace rangerDotNet;

public enum FileType
{
    File,
    Folder,
}

public record struct FileSystemEntry(String Name, String Path, FileType Type);
public record struct Folder(FileSystemEntry Self, FileSystemEntry[] Files, int FileCount)
{
    public override string ToString()
    {
        String str = "";
        str = str + $"Folder: {Self.Name} ({Self.Path})\n";
        for (int i = 0; i < FileCount; i++)
        {
            str = str + $"\t({i+1}) {Files[i].Name} [{Files[i].Type.ToString()}] ({Files[i].Path})\n";
        }
        return str;
    }
};
 