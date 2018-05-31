using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace AudioFileMassRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Path = @"C:\users\michaels\dropbox\FCBH2\";
            const string NewPath = @"C:\users\michaels\dropbox\FCBH3\";
            int bookNo = 0;

            foreach (var file in Directory.GetFiles(Path, "*.mp3"))
            {
                try
                {
                    var info = new FileInfo(file);
                    var source = info.FullName;
                    var dest = NewPath + info.Name.Substring(3);
                    Console.WriteLine("{0} => {1}", source, dest);
                    File.Copy(source, dest);
                    File.Delete(source);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }

            return;

            foreach (var file in Directory.GetFiles(Path, "*.mp3"))
            {
                var info = new FileInfo(file);
                var name = info.Name;
                var parts = name.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var prefix = parts[0];

                bookNo = int.Parse(prefix.Substring(1, 2));

                if (prefix.StartsWith("B"))
                {
                    bookNo += 39;
                }

                var chapter = parts[1];
                var book = parts[2];
                // var suffix = parts[3];

                var newFileName = String.Format("{0:00}_{1}_{2}.mp3", bookNo, book, chapter);
                Console.WriteLine(newFileName);

                try
                {
                    if (!Directory.Exists(NewPath))
                    {
                        Directory.CreateDirectory(NewPath);
                    }

                    Console.Write("Copying '{0}' from '{1}' to '{2}'...", name, Path + name, NewPath + newFileName);
                    File.Copy(Path + name, NewPath + newFileName);
                    Console.WriteLine("[DONE]");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }
        }
    }
}
