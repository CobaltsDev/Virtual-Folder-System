using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Query
    {
        static List<string> Vpath = new List<string> { "vfsbox" };
        static string XMLPath = "C:/Users/Izzy/Source/Repos/Virtual-Folder-System/ConsoleApplication1/ConsoleApplication1/vfsbox.xml";
        XDocument Doc = XDocument.Load(XMLPath);
        string Command = "";
        public Query(string command)
        {
            Command = command;
        }
        public void Parse()
        {
            switch(Command)
            {
                case "New":
                    Console.WriteLine("%>  Folder   :   Alias");
                    Console.Write("$> ");
                    string input = Console.ReadLine();
                    if (input == "Folder" || input == "folder")
                    {
                        int atList = 1;
                        Console.WriteLine("%>  Name?");
                        Console.Write("$> ");
                        string nameInput = Console.ReadLine();
                        XElement newElement = Doc.Descendants("vfsbox").FirstOrDefault();
                        while(atList < Vpath.Count)
                        {
                            newElement = Doc.Descendants(Vpath[atList]).FirstOrDefault();
                            atList++;
                        }
                        XElement toAdd = new XElement(nameInput);
                        toAdd.Add(new XAttribute("Type", "Folder"));
                        newElement.Add(toAdd);
                        XElement toSave;
                        if (newElement.Parent == null)
                        {
                            toSave = Doc.Element("vfsbox");
                        }
                        else
                        {
                            toSave = newElement.Parent;
                        }
                        while (toSave != Doc.Element("vfsbox"))
                        {
                            toSave = toSave.Parent;
                        }
                        toSave.Save(XMLPath);
                        Vpath.Add(nameInput);
                        Program.Terminal();
                    }
                    else if (input == "Alias" || input == "alias")
                    {
                        if(Vpath[Vpath.Count - 1] == "vsfbox")
                        {
                            Console.WriteLine("I'm sorry, you cannot have file aliases in the root vdirectory. Please create or open a folder");
                            Program.Terminal();
                        }
                        else
                        {
                            int atList = 1;
                            Console.WriteLine("%>  Description?");
                            Console.Write("$> ");
                            string name = Console.ReadLine();
                            XElement newElement = Doc.Descendants("vfsbox").FirstOrDefault();
                            while (atList < Vpath.Count)
                            {
                                newElement = Doc.Descendants(Vpath[atList]).FirstOrDefault();
                                atList++;
                            }
                            newElement.Add(new XElement(name));
                            newElement = Doc.Descendants(name).FirstOrDefault();
                            newElement.Add(new XAttribute("Type","Alias"));
                            Console.WriteLine("%>  Path?");
                            Console.Write("$> ");
                            string inputPath = Console.ReadLine();
                            newElement.Add(new XElement("Path", inputPath.Replace(@"\", "/")));
                            Random rng = new Random();
                            newElement.Add(new XElement("Index", ConvertToBase35(rng.Next(1, 36)) + ConvertToBase35(rng.Next(1, 36)) + ConvertToBase35(rng.Next(1, 36))));
                            XElement toSave;
                            if (newElement.Parent == null)
                            {
                                toSave = Doc.Element("vfsbox");
                            }
                            else
                            {
                                toSave = newElement.Parent;
                            }
                            while (toSave != Doc.Element("vfsbox"))
                            {
                                toSave = toSave.Parent;
                            }
                            toSave.Save(XMLPath);
                            Program.Terminal();
                        }
                    }
                    break;
                
                default:
                    Program.Terminal();
                    break;
            }
        }
        string ConvertToBase35(int from)
        {
            if(from <= 9)
            {
                return from.ToString(); ;
            }
            else
            {
                from -= 9;
                from = 26 - from;
                int ascii = 065 + from;
                char convAscii = (char)ascii;
                return convAscii.ToString();
            }
        }
    }
    class Program
    {
        public static void Terminal()
        {
            Console.Write("$> ");
            string input = Console.ReadLine();
            Query query = new Query(input);
            query.Parse();
        }
        static void Main(string[] args)
        {
            Terminal();
        }
    }
}
