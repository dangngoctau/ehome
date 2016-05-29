using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
#if MONO
using Mono.Data.Sqlite;
#endif

namespace EHome.Storage
{
    public class DbConnectionSelection : IDbConnectionSelection
    {

        public async Task<IDbConnection> OpenDbConnectionAsync(string path)
        {
#if MONO
            var connection = new SqliteConnection("URI=file:" + path);
            await connection.OpenAsync();
            return connection;
#else
            var connection = new SQLiteConnection("Data Source=" + path);
            await connection.OpenAsync();
            return connection;
#endif
        }
    }
}
