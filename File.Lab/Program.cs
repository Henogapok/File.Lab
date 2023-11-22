using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace File1.Lab
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
                char[] symb = { '-', ' ', '_', '.' };
                Console.WriteLine("Символы достпные для изменения: ");
                int tempCnt = 1;
                foreach(char x in symb)
                {
                    Console.WriteLine($"{tempCnt++}. {x}");
                }

                Dictionary<char, char> dict = GetNewSybmols(symb);
                string newPath = path + "New";
                CopyToOtherDirectory(path, newPath);
                ChangeFileName(newPath, dict);

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

        static Dictionary<char, char> GetNewSybmols(char[] oldSymbols)
        {
            Console.WriteLine("Выберите символы, которые вы хотите заменить:");
            int input;
            char res = 'y';
            Dictionary<char, char> dict = new Dictionary<char, char>();
            while (res != 'n')
            {
                if (!Int32.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Введите значения от 1 до 4");
                }
                else
                {
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("Введите символ, на который хотите заменить (Если хотите удалить элемент, просто нажмите Enter): ");
                            dict.Add(oldSymbols[input - 1], getNewChar());
                            break;
                        case 2:
                            Console.WriteLine("Введите символ, на который хотите заменить (Если хотите удалить элемент, просто нажмите Enter): ");
                            dict.Add(oldSymbols[input - 1], getNewChar());
                            break;
                        case 3:
                            Console.WriteLine("Введите символ, на который хотите заменить (Если хотите удалить элемент, просто нажмите Enter): ");
                            dict.Add(oldSymbols[input - 1], getNewChar());
                            break;
                        case 4:
                            Console.WriteLine("Введите символ, на который хотите заменить (Если хотите удалить элемент, просто нажмите Enter): ");
                            dict.Add(oldSymbols[input - 1], getNewChar());
                            break;
                    }
                    Console.WriteLine("Хотите выбрать еще один символ?(y/n)");
                    res = Console.ReadLine()[0];
                }
            }
            return dict;
        }
        static char getNewChar()
        {
            string newInput = Console.ReadLine();
            char charNewInput;
            if (string.IsNullOrEmpty(newInput))
                charNewInput = (char)0;
            else
                charNewInput = newInput[0];
            return charNewInput;
        }

        static void CopyToOtherDirectory(string from, string to)
        {
            
            if (!Directory.Exists(to))
                Directory.CreateDirectory(to);
            foreach(string s1 in Directory.GetFiles(path))
            {
                string s2 = to + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }

        }

        static void ChangeFileName(string newPath, Dictionary<char, char> newSymbols)
        {
            string oldName = "";
            string newName;
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            DirectoryInfo di = new DirectoryInfo(newPath);
            foreach(FileInfo fi in di.GetFiles())
            {
                oldName = fi.Name;
                char newChar;

                foreach(char x in newSymbols.Keys)
                {
                    if (oldName.Contains(x))
                    {
                        newSymbols.TryGetValue(x, out newChar);
                        if(newChar == 0)
                            oldName = oldName.Replace(x.ToString(), "");
                        else
                            oldName = oldName.Replace(x, newChar);
                    }
                }

                /*for(int i = 0; i < oldName.Length-3; i++)
                {
                    if (newSymbols.ContainsKey(oldName[i]))
                    {
                        newSymbols.TryGetValue(oldName[i], out newChar);
                        if(newChar == 0)
                            oldName.Remove(i);
                        else
                            oldName.Replace(oldName[i], newChar);
                    }
                }*/
                newName = oldName;
                try
                {
                    File.Move(fi.FullName, newPath + "\\" + newName);
                }
                catch(Exception E)
                {
                    Console.WriteLine("Файл не переименован! {0}", E.ToString());
                }
            }
        }
        
        
    }
}
