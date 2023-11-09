using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class UserController
    {
        
        public void UserMenu()
        {
            UserMethods methods = new UserMethods();
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();
                Console.WriteLine("Hvad vil du?");
                Console.WriteLine();
                Console.WriteLine("1: Søge efter person");
                Console.WriteLine("2: Søge efter titel");
                Console.WriteLine("3: Tilføj en titel");
                Console.WriteLine("4: Tilføj en person");
                Console.WriteLine("5: Slet en titel");
                Console.WriteLine("6: Gå tilbage");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        methods.SearchNames();
                        break;
                    case "2":
                        Console.Clear();
                        methods.SearchTitles();
                        break;
                    case "3":
                        Console.Clear();
                        methods.AddTitle();
                        break;
                    case "4":
                        Console.Clear();
                        methods.AddPerson();
                        break;
                    case "5":
                        Console.Clear();
                        methods.DeleteTitle();
                        break;
                    case "6":
                        continueProgram = false;
                        break;
                    default:
                        break;
                }
            }

        }

        
    }
}
