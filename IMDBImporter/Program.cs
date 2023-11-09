using IMDBDataImporter;
using IMDBImporter;
using System.Data.SqlClient;


string connString = "server=localhost;database=IMDB;user id=sa;password=Malthe2019;TrustServerCertificate=True";


bool continueProgram = true;

while (continueProgram)
{

    Console.Clear();
    Console.WriteLine("Velkommen til IMDB");
    Console.WriteLine();
    Console.WriteLine("1: Administrator");
    Console.WriteLine("2: Bruger");
    Console.WriteLine("3: Luk program");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Clear();
            AdminController admin = new();
            admin.AdminMenu();
            break;
        case "2":
            Console.Clear();
            UserController user = new();
            user.UserMenu();
            break;
        case "3":
            continueProgram = false;
            break;
        default:
            Console.WriteLine($"{input} is not a valid option.");
            Console.WriteLine();
            break;
    }
}





















