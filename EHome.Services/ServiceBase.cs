using System.Data.SQLite;

namespace EHome.Services
{
    public abstract class ServiceBase
    {
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=" + Database.GetDbPath());
        }
    }
}
