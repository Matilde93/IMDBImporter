using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class CrewInserter
    {

        public static void InsertCrew(SqlConnection sqlConn, List<Crew> crewsList)
        {
            DataTable directorsTable = new DataTable();
            directorsTable.Columns.Add("TitleID", typeof(string));
            directorsTable.Columns.Add("NameID", typeof(string));

            DataTable writersTable = new DataTable();
            writersTable.Columns.Add("TitleID", typeof(string));
            writersTable.Columns.Add("NameID", typeof(string));

            // Populate the DataTable with your data
            foreach (Crew crew in crewsList)
            {
                if (crew.directors != null && crew.directors.Count > 0)
                {
                    foreach (string nameID in crew.directors)
                    {
                        if (nameID.Length > 2)
                        {
                            directorsTable.Rows.Add(crew.titleID, nameID);
                        }
                    }
                }
                if (crew.writers != null && crew.writers.Count > 0)
                {
                    foreach (string nameID in crew.writers)
                    {
                        if (nameID.Length > 2)
                        {
                            writersTable.Rows.Add(crew.titleID, nameID);
                        }
                    }
                }
            }

            SqlBulkCopy bulkCopyDirectors = new SqlBulkCopy(sqlConn);
            bulkCopyDirectors.DestinationTableName = "Titles_Directors";
            bulkCopyDirectors.BulkCopyTimeout = 0;
            bulkCopyDirectors.WriteToServer(directorsTable);

            SqlBulkCopy bulkCopyWriters = new SqlBulkCopy(sqlConn);
            bulkCopyWriters.DestinationTableName = "Titles_Writers";
            bulkCopyWriters.BulkCopyTimeout = 0;
            bulkCopyWriters.WriteToServer(writersTable);
        }
    }
}
