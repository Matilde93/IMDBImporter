using System.Data.SqlClient;

namespace IMDBImporter
{
    public interface IInserter
    {
        void InsertTitles(SqlConnection sqlconn, List<Title> titles);
        void InsertNames(SqlConnection sqlConn, List<Name> names);
    }
}



