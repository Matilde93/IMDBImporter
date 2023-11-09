using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    internal class BulkInserter : IInserter
    {
        public void InsertTitles(SqlConnection sqlconn, List<Title> titles)
        {
            DataTable titleTable = new DataTable("Titles");
            

            titleTable.Columns.Add("titleID", typeof(string));
            titleTable.Columns.Add("titleType", typeof(string));
            titleTable.Columns.Add("primaryTitle", typeof(string));
            titleTable.Columns.Add("originalTitle", typeof(string));
            titleTable.Columns.Add("isAdult", typeof(bool));
            titleTable.Columns.Add("startYear", typeof(int));
            titleTable.Columns.Add("endYear", typeof(int));
            titleTable.Columns.Add("runTimeMinutes", typeof(int));

            


            foreach(Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                FillParameter(titleRow, "titleID", title.titleID);
                FillParameter(titleRow, "titleType", title.titleType);
                FillParameter(titleRow, "primaryTitle", title.primaryTitle);
                FillParameter(titleRow, "originalTitle", title.originalTitle);
                FillParameter(titleRow, "isAdult", title.isAdult);
                FillParameter(titleRow, "startYear", title.startYear);
                FillParameter(titleRow, "endYear", title.endYear);
                FillParameter(titleRow, "runTimeMinutes", title.runTimeMinutes);
                titleTable.Rows.Add(titleRow);


              
            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(titleTable);

        }


        public void InsertNames(SqlConnection sqlconn, List<Name> names)
        {
            DataTable namesTable = new DataTable("Names");
            namesTable.Columns.Add("nameID", typeof(string));
            namesTable.Columns.Add("name", typeof(string));
            namesTable.Columns.Add("birthYear", typeof(int));
            namesTable.Columns.Add("deathYear", typeof(int));



            foreach (Name name in names)
            {
                DataRow namesRow = namesTable.NewRow();
                FillParameter(namesRow, "nameID", name.nameID);
                FillParameter(namesRow, "name", name.name);
                FillParameter(namesRow, "birthYear", name.birthYear);
                FillParameter(namesRow, "deathYear", name.deathYear);
                namesTable.Rows.Add(namesRow);

            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Names";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(namesTable);

        }

        public void FillParameter(DataRow row, string columnName, object? value)
        {
            if (value != null)
            {
                row[columnName] = value;
            }
            else
            {
                row[columnName] = DBNull.Value;
            }
        }
    }
}
