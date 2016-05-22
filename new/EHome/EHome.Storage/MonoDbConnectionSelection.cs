using System.Data;
using Mono.Data.Sqlite;

namespace EHome.Storage
{
    public class MonoDbConnectionSelection : IDbConnectionSelection
    {
        public IDbConnection GetDbConnection(string path)
        {
            return new SqliteConnection("URI=file:" + path);
        }
    }
}
