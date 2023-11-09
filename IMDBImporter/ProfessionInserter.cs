using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class ProfessionInserter
    {
        public static void InsertProfessions(SqlConnection sqlConn,
            List<Name> namesList)
        {
            HashSet<string> professions = new HashSet<string>();
            Dictionary<string, int> professionDict =
                new Dictionary<string, int>();
            
            foreach (var name in namesList)
            {
                if(name.professions != null)
                {
                    foreach (var profession in name.professions)
                    {
                        professions.Add(profession);
                    }
                }
                
            }

            foreach (string profession in professions)
            {
                SqlCommand sqlComm = new SqlCommand(
                    "INSERT INTO Professions(Profession)" +
                    "OUTPUT INSERTED.ProfessionID " +
                    "VALUES ('" + profession + "')", sqlConn);
                try
                {
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                        int newId = (int)reader["ProfessionID"];
                        professionDict.Add(profession, newId);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(sqlComm.CommandText, ex);
                }

            }

            foreach (Name myNames in namesList)
            {
                if(myNames.professions != null)
                {
                    foreach (string profession in myNames.professions)
                    {
                        SqlCommand sqlComm = new SqlCommand(
                            "INSERT INTO Names_Professions (NameID, ProfessionID)" +
                            " VALUES " +
                            "('" + myNames.nameID + "', '"
                            + professionDict[profession] + "')", sqlConn);
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
}