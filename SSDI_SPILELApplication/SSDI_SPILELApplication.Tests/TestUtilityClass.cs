using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SSDI_SPILELApplication.Tests
{
    public class TestUtilityClass
    {
        private static string dataBaseName = "spielDBTest";
        public static IMongoDatabase CreateDataConnection(MongoClient obj)
        {
            return obj.GetDatabase(dataBaseName);
        }
    }
}
