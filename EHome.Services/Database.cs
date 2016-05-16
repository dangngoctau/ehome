using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Services
{
    public static class Database
    {
        public static string GetDbPath()
        {
            return Environment.CurrentDirectory + "\\Test.sqlite";
        }

        public static void Create()
        {
            var dbPath = GetDbPath();
            if (File.Exists(dbPath))
            {
                return;
            }

            using (var connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                connection.Execute(@"CREATE TABLE [Devices](
                                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                                        [Name] varchar(100) NOT NULL);");
            }
        }
    }
}
