using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Lab
{
    internal class Program
    {
        static string path = @"C:\Users\d3n1s\Documents\TNSShort";
        static void Main(string[] args)
        {
            Console.WriteLine("Существующие расширения: ");
            foreach(string x in GetFilesExtensions())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("Введите нужное расширение (.название_расширения): ");

            List<string> extensions = new List<string>();

            extensions = Console.ReadLine().Split(',').ToList();
            GetFilesExtensions();
            Console.WriteLine("Все файлы с данным расширением: ");
            List<FileInfo> filesByExtensions = GetFilesByExtension(extensions);
            foreach (FileInfo x in filesByExtensions)
            {
                Console.WriteLine(x.Name);
            }
            Console.WriteLine("Общее количество файлов: {0}", filesByExtensions.Count);
            Console.WriteLine("Разрешить работу с файлами?(y/n)");
            char res = Console.ReadLine()[0];
            if (res == 'n')
                return;
            else
            {
                Console.WriteLine("Введите символы, которые вы хотите заменить, через пробел:");
                List<string> symb = Console.ReadLine().Split(' ').ToList();
                
                foreach(string x in symb)
                {
                    Console.WriteLine("Как вы хотите заменить {0}?", x);

                }
            }
            
        }
        
        static HashSet<string> GetFilesExtensions()
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<FileInfo> fi= di.GetFiles().ToList();
            HashSet<string> extensionsSet = new HashSet<string>();
            foreach(FileInfo item in fi)
                extensionsSet.Add(item.Extension);
            /*foreach(string str in extensionsSet)
                Console.WriteLine(str);*/
            
            return extensionsSet;
        }

        static List<FileInfo> GetFilesByExtension(List<string> extensions)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<FileInfo> fi = new List<FileInfo>();
            int cnt = 0;
            foreach(string x in extensions)
            {
                fi.AddRange(di.GetFiles().Where(w => w.Extension.Equals(x)));
                cnt++;
            }
            return fi;
        }
        
        
    }
}
