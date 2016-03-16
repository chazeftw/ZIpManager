using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ZipManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ideas ___
            // Extract all zip files in a directory into respective folders with same name as archive
            // Create zip file and store copy all files from one directory into it
            // 

            string pathstart = Directory.GetCurrentDirectory();
            
            string[] folders = Directory.GetDirectories(Directory.GetCurrentDirectory());

            int counter = 0;
            int state = 0;
            string input = "";
            int result;

            while (state != -1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MENU");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Create single .zip of current directory (created in parent directory)");
                Console.WriteLine("2. Create .zip files of current subdirectories");
                Console.WriteLine("3. Exit");
                input = Console.ReadLine();
                bool isInt = int.TryParse(input, out result);
                if (isInt)
                {
                    state = int.Parse(input);
                }
                switch (state)
                {
                    case 1:
                        createSingleZip(pathstart);
                        Console.WriteLine();
                        break;
                    case 2:
                        createMultipleZips(folders, pathstart, counter);
                        Console.WriteLine();
                        break;
                    case 3:
                        System.Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("Not a valid input..");
                        break;
                }
            }
            

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(".zip files created: "+counter);
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }

        static void createMultipleZips(string[] folders, string pathstart, int counter)
        {
            foreach (string folder in folders)
            {
                if (File.Exists(folder + ".zip"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Overwriting "+folder+".zip");
                    File.Delete(folder+".zip");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Putting folder: {0} into: {1}", folder.Substring(pathstart.Length + 1), folder.Substring(pathstart.Length + 1) + ".zip");
                ZipFile.CreateFromDirectory(folder, pathstart + folder.Substring(pathstart.Length) + ".zip");
                counter++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void createSingleZip(string pathstart)
        {
            if(File.Exists(Directory.GetParent(pathstart)+"/new.zip"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Overwriting old .zip folder..");
                File.Delete(Directory.GetParent(pathstart) + "/new.zip");
            }
            ZipFile.CreateFromDirectory(pathstart, Directory.GetParent(pathstart)+"/new.zip",CompressionLevel.Fastest,true);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Copy of directory succesfully added to new.zip..");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
