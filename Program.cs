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
            Settings settings = IO.loadSetting(@"C:\Users\canti\Documents\C#\rangerDotNet\settings.json");
            Console.WriteLine(settings.Next[1].KeyName);
            Console.WriteLine(settings.Prev[0].KeyString);
            Console.WriteLine(settings.Shell);

            //FileSelector selector = new FileSelector();
            //FileSystemEntry? ent = selector.currentFolder.Self;
            //while (ent != null && ent.Value.Type == FileType.Folder)
            //{
            //    ent = selector.Select();
            //    if (ent != null && ent.Value.Type == FileType.Folder)
            //    {
            //        System.IO.Directory.SetCurrentDirectory(ent.Value.Path);
            //        selector = new FileSelector();
            //    }
            //}
            //if (ent != null)
            //{
            //    Console.Clear();
            //    Console.WriteLine(ent.Value.Path);
            //    Console.WriteLine(ent.Value.Name);
            //}

            //ConsoleKeyInfo pressedKey = Console.ReadKey();
            //Console.WriteLine(pressedKey.Key);
            //Console.WriteLine($"\"{pressedKey.KeyChar}\"");
            //Console.WriteLine(pressedKey.Modifiers);

        }

   

    }
}