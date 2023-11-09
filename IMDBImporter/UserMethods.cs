using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class UserMethods 
    {
        //Virker, men har en mindre fejl. Den viser både den nye søgning og den tidligere. Uanset metoden. viser bare altid tidligere resultater.
        string connString = "server=localhost;database=IMDB;user id=sa;password=Malthe2019;TrustServerCertificate=True";
        
        public void SearchNames() 
        {
            Console.Clear();
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            Console.WriteLine("Indtast navn du vil søge efter..");
            string inputName = Console.ReadLine();

            SqlCommand commandNames = new SqlCommand($"SELECT [Name] FROM [Names] WHERE LOWER([Name]) LIKE '%{inputName}%' ORDER BY [Name] ASC", sqlConn);
            
            SqlDataReader namesReader = commandNames.ExecuteReader();


            while (namesReader.Read())
            {
                Console.WriteLine(namesReader["Name"].ToString());
            }


            Console.WriteLine("Tryk på en tast for at vende tilbage..");
            Console.ReadKey();
            sqlConn.Close();

        }


        public void SearchTitles()
        {
            Console.Clear();
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            Console.WriteLine("Indtast titel du vil søge efter..");
            string input = Console.ReadLine();

            SqlCommand command = new SqlCommand($"SELECT [PrimaryTitle] FROM [Titles] WHERE LOWER([PrimaryTitle]) LIKE '%{input}%' ORDER BY [PrimaryTitle] ASC", sqlConn);
            

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["PrimaryTitle"].ToString());
            }

            reader.Close();
            Console.WriteLine("Tryk for at vende tilbage..");
            Console.ReadKey();
            sqlConn.Close();

        }

        public void AddTitle()
        {
            Console.Clear();
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            SqlCommand highestId = new SqlCommand("SELECT MAX(TitleID) FROM Titles", sqlConn);
            string currentMaxIdString = (string)highestId.ExecuteScalar();
            int currentMaxId = Convert.ToInt32(currentMaxIdString.Trim('t'));


            int isAdultBit;

            Console.WriteLine("Tilføj titel: ");
            Console.WriteLine("Indtast Title Type");
            string titleType = Console.ReadLine();
            if (titleType == null)
            {
                Console.WriteLine("Title Type må ikke være null, skriv venligst en Title Type");
                titleType = Console.ReadLine();
            }

            Console.WriteLine("Indtast Primary Title");
            string primaryTitle = Console.ReadLine();
            if (primaryTitle == null)
            {
                Console.WriteLine("Primary Title må ikke være null, skriv venligst en Primary Title");
                primaryTitle = Console.ReadLine();
            }

            Console.WriteLine("Indtast Original Title");
            string originalTitle = Console.ReadLine();
            if (originalTitle == null)
            {
                Console.WriteLine("Original Title må ikke være null, skriv venligst en Original Title");
                originalTitle = Console.ReadLine();
            }

            Console.WriteLine("Er det en voksen film? Y/N");
            string isAdult = Console.ReadLine();
            if(isAdult.ToLower() != "y" && isAdult.ToLower() != "n")
            {
                Console.WriteLine("Du tastede forkert. Venligst tryk 'Y' for ja eller 'N' for nej");
                isAdult = Console.ReadLine();
            }

            Console.WriteLine("Indtast Start Year");
            int startYear = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast End Year");
            int endYear = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast Run Time in Minutes");
            int runTime = Convert.ToInt32(Console.ReadLine());

            if(isAdult == "y")
            {
                isAdultBit = 1;
            } else
            {
                isAdultBit = 0;
            }
             


            SqlCommand insertCommand = new SqlCommand($"INSERT INTO [dbo].[Titles] ([TitleID],[TitleType]," +
                $"[PrimaryTitle],[OriginalTitle],[IsAdult],[StartYear],[EndYear],[RunTimeMinutes]) " +
                $"VALUES('tt{++currentMaxId}', '{titleType}', '{primaryTitle}', '{originalTitle}'," +
                $" {isAdultBit}, {startYear}, {endYear}, {runTime})", sqlConn);
            try
            {
                insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(insertCommand.CommandText, ex);
            }

            
            Console.WriteLine("Tryk for at vende tilbage..");
            Console.ReadKey();
            sqlConn.Close();
        }

        public void AddPerson()
        {
            Console.Clear();
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            SqlCommand highestId = new SqlCommand("SELECT MAX(NameID) FROM Names", sqlConn);
            string currentMaxIdString = (string)highestId.ExecuteScalar();
            int currentMaxId = Convert.ToInt32(currentMaxIdString.Trim('n', 'm'));

            Console.WriteLine(currentMaxId);
            Console.ReadKey();

            SqlCommand insertCommand;
            

            Console.WriteLine("Tilføj person: ");
            Console.WriteLine("Indtast Name");
            string? name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Name må ikke være null, skriv venligst et Name");
                name = Console.ReadLine();
            }

            Console.WriteLine("Indtast Birth Year, hvis relevant.");
            string? inputBirthYear = Console.ReadLine();
            

            Console.WriteLine("Indtast Death Year, hvis relevant.");
            string? inputDeathYear = Console.ReadLine();


            if(inputBirthYear != "" && inputDeathYear != "")
            {
                insertCommand = new SqlCommand($"INSERT INTO [dbo].[Names] ([NameID], [Name],[BirthYear],[DeathYear]) " +
                $"VALUES('nm{++currentMaxId}', '{name}', {Convert.ToInt32(inputBirthYear)}, {Convert.ToInt32(inputDeathYear)})", sqlConn);
            }
            else if (inputBirthYear != "" && inputDeathYear == "")
            {
                insertCommand = new SqlCommand($"INSERT INTO [dbo].[Names] ([NameID], [Name],[BirthYear]) " +
                $"VALUES('nm{++currentMaxId}', '{name}', {Convert.ToInt32(inputBirthYear)})", sqlConn);
            }
            else if (inputBirthYear == "" && inputDeathYear != "")
            {
                insertCommand = new SqlCommand($"INSERT INTO [dbo].[Names] ([NameID], [Name], [DeathYear]) " +
                $"VALUES('nm{++currentMaxId}', '{name}', {Convert.ToInt32(inputDeathYear)})", sqlConn);
            }
            else
            {
                insertCommand = new SqlCommand($"INSERT INTO [dbo].[Names] ([NameID], [Name]) " +
               $"VALUES('nm{++currentMaxId}', '{name}')", sqlConn);
            }

            try
            {
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Person blev tilføjet..");
            }
            catch (Exception ex)
            {
                throw new Exception(insertCommand.CommandText, ex);
            }


            Console.WriteLine("Tryk for at vende tilbage..");
            Console.ReadKey();
            sqlConn.Close();
        }

        public void DeleteTitle()
        {
            Console.Clear();

            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            Console.WriteLine("Slet en Titel:");
            Console.WriteLine("Indtast Titel ID på den titel du gerne vil slette.");
            string titelID = Console.ReadLine();

            SqlCommand Command = new SqlCommand($"SELECT [TitleID], [PrimaryTitle] FROM Titles WHERE TitleID = '{titelID}'", sqlConn);
            SqlDataReader reader = Command.ExecuteReader();
            reader.Read();
            
            
            try
            {
                Console.WriteLine("Bekræft at denne Titel skal slettes. Tryk Y for Ja. Tryk N for Nej.");
                Console.WriteLine("TitelID: " + reader["TitleID"].ToString() + " Primary Title: " + reader["PrimaryTitle"].ToString());
                reader.Close();
                string confirmString = Console.ReadLine();
                if( confirmString.Trim().ToLower() == "y" )
                {
                    SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Titles WHERE TitleID = '{titelID}'", sqlConn);
                    deleteCommand.ExecuteNonQuery();
                    Console.WriteLine("Titel blev slettet.");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(Command.CommandText, ex);
            }


            Console.WriteLine("Tryk for at vende tilbage..");
            Console.ReadKey();
            sqlConn.Close();


        }
    }
}
