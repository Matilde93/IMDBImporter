using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IMDBImporter
{
    public class KnownForTitleInserter
    {
        public static void InsertKnownForTitle(SqlConnection sqlConn, List<Name> namesList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NameID", typeof(string));
            dataTable.Columns.Add("TitleID", typeof(string));

            // Populate the DataTable with your data
            foreach (Name name in namesList)
            {
                if (name.knownForTitles != null && name.knownForTitles.Count > 0)
                {
                    foreach (string title in name.knownForTitles)
                    {
                        if (title.Length > 2)
                        {
                            dataTable.Rows.Add(name.nameID, title);
                        }
                    }
                }
            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Titles_Names";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(dataTable);
        }

    }
}