using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class GenreInserter
    {
        public static void InsertGenres(SqlConnection sqlConn,
            List<Title> titleList)
        {
            HashSet<string> genres = new HashSet<string>();
            Dictionary<string, int> genreDict =
                new Dictionary<string, int>();
            foreach (var title in titleList)
            {
                foreach (var genre in title.genres)
                {
                    genres.Add(genre);
                }
            }
            foreach (string genre in genres)
            {
                SqlCommand sqlComm = new SqlCommand(
                    "INSERT INTO Genres(genre)" +
                    "OUTPUT INSERTED.GenreID " +
                    "VALUES ('" + genre + "')", sqlConn);
                try
                {
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                        int newId = (int)reader["GenreID"];
                        genreDict.Add(genre, newId);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(sqlComm.CommandText, ex);
                }

            }

            foreach (Title myTitle in titleList)
            {
                foreach (string genre in myTitle.genres)
                {
                    SqlCommand sqlComm = new SqlCommand(
                        "INSERT INTO Titles_Genres (TitleID, GenreID)" +
                        " VALUES " +
                        "('" + myTitle.titleID + "', '"
                        + genreDict[genre] + "')", sqlConn);
                    try
                    {
                        sqlComm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(sqlComm.CommandText, ex);
                    }
                }

            }
        }
    }
}