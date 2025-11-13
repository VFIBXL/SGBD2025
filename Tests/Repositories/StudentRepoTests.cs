using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;
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
    public class StudentRepoTests
    {
        private readonly Shared.DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup dBSetup => _fixture.DbSetup;

        public StudentRepoTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _connectionString = _fixture.ConnectionString;
        }

        [Fact]
        public async Task GetAllTest()
        {
            await dBSetup.InitStudentsDataAsync();

            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.StudentRepo>.Instance;
            var repo = new Repositories.StudentRepo(logger, _connectionString);

            // act
            var students = repo.GetAll();

            // assert
            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.Equal(3, students.Count);

        }

        [Theory]
        [InlineData("Fievez", 1)]
        [InlineData("Dupont", 0)]
        [InlineData("%o%", 2)]
        public async Task FindStudentsByLastName(string search, int result)
        {
            await dBSetup.InitStudentsDataAsync();

            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.StudentRepo>.Instance;
            var repo = new Repositories.StudentRepo(logger, _connectionString);

            // act
            var students = repo.FindStudentsByLastName(search);

            Assert.Equal(result, students.Count);
        }
    }
}
