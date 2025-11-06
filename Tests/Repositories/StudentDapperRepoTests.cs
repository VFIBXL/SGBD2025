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

namespace Tests.RepositoriesTests
{
    public class StudentDapperRepoTests
    {
        private MsSqlContainer _container;

        [Fact]
        public async Task GetAllTest()
        {
            var container = new MsSqlBuilder()
               .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
               .WithPassword("Your_strong(!)Password") // MSSQL demande un mot de passe fort
               .Build();

            await container.StartAsync();

            DBSetup dBSetup = new DBSetup(container.GetConnectionString());

            await dBSetup.CreateDBAsync();

            await dBSetup.CreatTablesAsync();

            await dBSetup.InitStudentsDataAsync();

            try
            {
                string connectionString = container.GetConnectionString();
                connectionString = connectionString.Replace("master", "CoursSGBD");
                

                // instantiate repo with NullLogger and injected connection string
                var logger = NullLogger<Repositories.StudentDapperRepo>.Instance;
                var repo = new Repositories.StudentDapperRepo(logger, connectionString);

                // act
                var students = repo.GetAll();

                // assert
                Assert.NotNull(students);
                Assert.NotEmpty(students);
                Assert.Equal(3, students.Count);
            }
            finally
            {
                await container.DisposeAsync();
            }
        }

        [Theory]
        [InlineData("Fievez", 1)]
        [InlineData("Dupont", 0)]
        [InlineData("%o%", 2)]
        public async Task FindStudentsByLastName(string search, int result)
        {
            var container = new MsSqlBuilder()
               .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
               .WithPassword("Your_strong(!)Password") // MSSQL demande un mot de passe fort
               .Build();

            await container.StartAsync();

            DBSetup dBSetup = new DBSetup(container.GetConnectionString());

            await dBSetup.CreateDBAsync();

            await dBSetup.CreatTablesAsync();

            await dBSetup.InitStudentsDataAsync();

            try
            {
                string connectionString = container.GetConnectionString();
                connectionString = connectionString.Replace("master", "CoursSGBD");


                // instantiate repo with NullLogger and injected connection string
                var logger = NullLogger<Repositories.StudentDapperRepo>.Instance;
                var repo = new Repositories.StudentDapperRepo(logger, connectionString);

                // act
                var students = repo.FindStudentsByLastName(search);

                Assert.Equal(result, students.Count);
            }
            finally
            {
                await container.DisposeAsync();
            }
        }
    }
}
