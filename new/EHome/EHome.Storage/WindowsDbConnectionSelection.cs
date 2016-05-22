using System.Data;
using System.Data.SQLite;
namespace EHome.Storage
{
    public class WindowsDbConnectionSelection : IDbConnectionSelection
    {
        public IDbConnection GetDbConnection(string path)
        {
            return new SQLiteConnection("Data Source=" + path);
        }
    }
}
