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
        static string XMLPath = "C:/Users/Izzy/Source/Repos/Virtual-Folder-System/ConsoleApplication1/ConsoleApplication1/vfsbox.xml";
        XDocument Doc = XDocument.Load(XMLPath);
        string Scope = "vsfbox"; 
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
                    Console.WriteLine("%>  Folder");
                    Console.Write("$> ");
                    string input = Console.ReadLine();
                    if (input == "Folder" || input == "folder")
                    {
                        Console.Write("%>  Name?");
                        Console.WriteLine("");
                        Console.Write("$> ");
                        XElement newElement = new XElement(Console.ReadLine());
                        newElement.Add(new XAttribute("Type", "Folder"));
                        newElement.Add(new XElement("ROOT"));
                        Doc.Element(Scope).Add(newElement);
                        Doc.Save(XMLPath);
                    }
                    break;
                default:
                    Program.Terminal();
                    break;
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
