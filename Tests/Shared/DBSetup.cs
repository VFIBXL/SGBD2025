
using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Shared
{
    public class DBSetup : BaseRepo
    {
        private readonly string _connectionString;

        public DBSetup(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public async Task CreateDBAsync()
        {
            await RunScript("CreateDB.sql");
        }

        public async Task CreatTablesAsync()
        {
            await RunScript("CreateTables.sql");
        }

        public async Task InitStudentsDataAsync()
        {
            await RunScript("InitStudentsData.sql");
        }

        private async Task RunScript(string fileName)
        {
            string sql = GetFileFromAssemblyAsync(fileName);
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                int rowsAffected = await connection.ExecuteAsync(sql);
            }
        }
    }
}
