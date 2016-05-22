using Dapper;
using EHome.Storage.Models;
using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Linq;

namespace EHome.Storage
{
    public class EHomeService : IEHomeService
    {
        private readonly IDbConnectionSelection _dbConnectionSelection;

        public EHomeService(IDbConnectionSelection dbConnectionSelection)
        {
            _dbConnectionSelection = dbConnectionSelection;
        }

        public IEnumerable<Device> GetDevices()
        {
            using (var connection = _dbConnectionSelection.GetDbConnection("EHome.sqlite"))
            {
                connection.Open();
                var result = connection.Query<Device>(@"select * from Devices");
                Console.WriteLine("total devices is " + result.Count());
                return result;
            }
        }
    }
}
