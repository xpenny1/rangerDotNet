using System.Runtime.InteropServices;

namespace rangerDotNet
{
    class Program    {

        static int fib(int i) => i switch {
                1 => 1,
                2 => 1,
                _ => fib(i-2) + fib(i-1),
            };

        static void Main(string[] args)
        {
            
            FileSelector selector = new FileSelector();
            FileSystemEntry ent = selector.currentFolder.Self;
            while (ent.Type == FileType.Folder)
            {
                ent = selector.Select();
                if (ent.Type == FileType.Folder)
                {
                    System.IO.Directory.SetCurrentDirectory(ent.Path);
                    selector = new FileSelector();
                }
            }
            Console.Clear();
            Console.WriteLine(ent.Path);
            Console.WriteLine(ent.Name);
        }

   

    }
}