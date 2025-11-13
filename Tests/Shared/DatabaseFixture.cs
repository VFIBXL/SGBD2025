using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace Tests.Shared
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private MsSqlContainer _container;
        public DBSetup DbSetup { get; private set; } = null!;
        public string ConnectionString { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("Your_strong(!)Password")
                .Build();

            await _container.StartAsync();

            // Build connection string and DBSetup
            var masterCs = _container.GetConnectionString();
            // ensure DB created, tables created and seeded once
            DbSetup = new DBSetup(masterCs);
            await DbSetup.CreateDBAsync();
            await DbSetup.CreatTablesAsync();
            await DbSetup.InitStudentsDataAsync();

            ConnectionString = masterCs.Replace("master", "CoursSGBD");
        }

        public async Task DisposeAsync()
        {
            if (_container is not null)
            {
                await _container.StopAsync();
                await _container.DisposeAsync();
                _container = null!;
            }
        }
    }


    [CollectionDefinition("IntegrationDB", DisableParallelization = true)]
    public class IntegrationCollection : ICollectionFixture<DatabaseFixture>
    {
        // No code here - just links the fixture to the collection name
    }
}
