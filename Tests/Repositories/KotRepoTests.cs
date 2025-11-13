using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;
using ModelsDLL.DTO;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Tests.Shared;

namespace Tests.RepositoriesTests
{
    [Collection("IntegrationDB")]
    public class KotRepoTests
    {
        private readonly Shared.DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup dBSetup => _fixture.DbSetup;

        public KotRepoTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _connectionString = _fixture.ConnectionString;
        }

        [Fact]
        public async Task GetAllTest()
        {
            await dBSetup.InitKotsDataAsync();

            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.KotRepo>.Instance;
            var repo = new Repositories.KotRepo(logger, _connectionString);

            // act
            var kots = repo.GetAll();

            // assert
            Assert.NotNull(kots);
            Assert.NotEmpty(kots);
            Assert.Equal(2, kots.Count);
        }

        [Fact]
        public async Task DeleteTest()
        {
            await dBSetup.InitKotsDataAsync();
            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.KotRepo>.Instance;
            var repo = new Repositories.KotRepo(logger, _connectionString);
            
            // act
            repo.Delete(1);
            // assert

            List<KotStudentDTO> kots = repo.GetAll();

            var kot = kots.FirstOrDefault( x=> x.KOT_ID == 1);

            Assert.Null(kot);
        }
    }
}
