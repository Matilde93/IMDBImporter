using IMDBDataImporter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class AdminController
    {

        public void AdminMenu()
        {
            string connString = "server=localhost;database=IMDB;user id=sa;password=Malthe2019;TrustServerCertificate=True";

            int numberOfLines = 1000000;


            List<Title> titles = new List<Title>();

            foreach (string line in System.IO.File.ReadLines("C:\\Users\\matil\\OneDrive\\Datamatiker\\4.Semester\\IMDB\\data.tsv").Skip(1).Take(numberOfLines))
            {
                string[] values = line.Split("\t");
                if (values.Length == 9)
                {
                    titles.Add(new Title(
                        values[0], values[1], values[2], values[3],
                        ConvertToBool(values[4]), ConvertToInt(values[5]),
                        ConvertToInt(values[6]), ConvertToInt(values[7]),
                        values[8]
                        ));
                }
            }

            List<Name> names = new List<Name>();

            foreach(string line in System.IO.File.ReadLines("C:\\Users\\matil\\OneDrive\\Datamatiker\\4.Semester\\IMDB\\name.data.tsv").Skip(1).Take(numberOfLines))
            {
                string[] values = line.Split("\t");
                if (values.Length == 6)
                {
                    names.Add(new Name(
                        values[0], values[1], ConvertToInt(values[2]), ConvertToInt(values[3]), values[4], values[5]
                        ));
                }
            }

            List<Crew> crew = new List<Crew>();

            foreach (string line in System.IO.File.ReadLines("C:\\Users\\matil\\OneDrive\\Datamatiker\\4.Semester\\IMDB\\crew.data.tsv").Skip(1).Take(numberOfLines))
            {
                string[] values = line.Split("\t");
                if (values.Length == 3)
                {
                    crew.Add(new Crew(
                        values[0], values[1], values[2]
                        ));
                }
            }

            Console.WriteLine("Number of lines to insert " + titles.Count);
            SqlConnection sqlConn = new SqlConnection(connString);



            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();
                IInserter? myInserter = null;

                Console.WriteLine("Hvad vil du?");
                Console.WriteLine("0:  status på databasen");
                Console.WriteLine("q: gå tilbage til hovedmenu");
                Console.WriteLine("1: slette alt");
                Console.WriteLine("2: Normal Insert");
                Console.WriteLine("3: Prepared Insert");
                Console.WriteLine("4: Bulk Insert");


                string input = Console.ReadLine();



                sqlConn.Open();
                switch (input.ToLower())
                {
                    case "q":
                        continueProgram = false;
                        break;
                    case "0":
                        Console.WriteLine("Status på Databasen er: ");
                        string query = "SELECT COUNT(*) FROM Titles";
                        SqlCommand comm = new SqlCommand(query, sqlConn);
                        int count = (int)comm.ExecuteScalar();
                        Console.WriteLine("Der er " + count + " linjer i databasen");
                        Console.ReadKey();
                        break;
                    case "1":
                        SqlCommand cmd = new SqlCommand("DELETE FROM Titles_Genres; " + "DELETE FROM Genres; " + "DELETE FROM Titles; " + "DELETE FROM Names_Professions; "
                            + "DELETE FROM Titles_Names; " + "DELETE FROM Names; " + "DELETE FROM Professions; ", sqlConn);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alle linjer blev slettet...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Inserting data...");
                        myInserter = new NormalInserter();
                        break;
                    case "3":
                        Console.WriteLine("Inserting data...");
                        myInserter = new PreparedInserter();
                        break;
                    case "4":
                        Console.WriteLine("Inserting data...");
                        myInserter = new BulkInserter();
                        break;

                }

                DateTime before = DateTime.Now;

                if (myInserter != null)
                {
                    //myInserter.InsertTitles(sqlConn, titles);
                    //GenreInserter.InsertGenres(sqlConn, titles);

                    //myInserter.InsertNames(sqlConn, names);
                    //ProfessionInserter.InsertProfessions(sqlConn, names);
                    //KnownForTitleInserter.InsertKnownForTitle(sqlConn, names);
                    CrewInserter.InsertCrew(sqlConn, crew);
                }



                sqlConn.Close();

                DateTime after = DateTime.Now;

                Console.WriteLine("Tid: " + (after - before));
                Console.ReadKey();

            }
        }

        bool ConvertToBool(string input)
        {
            if (input == "0")
            {
                return false;
            }
            else if (input == "1")
            {
                return true;
            }
            throw new ArgumentException(
                "Kolonne er ikke 0 eller 1, men " + input);
        }

        int? ConvertToInt(string input)
        {
            if (input.ToLower() == @"\n")
            {
                return null;
            }
            else
            {
                return int.Parse(input);
            }

        }

    }
}
