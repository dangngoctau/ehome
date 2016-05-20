using Dapper;
using EHome.Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Storage
{
    public class EHomeService : IEHomeService
    {
        public IEnumerable<Device> GetDevices()
        {
            using (var connection = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "\\Test.sqlite"))
            {
                connection.Open();
                var result = connection.Query<Device>(@"select * from devices");

                return result;
            }
        }
    }
}
