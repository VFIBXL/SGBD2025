using ADONET.Interfaces;
using ADONET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Repositories
{
    public class CoursSGBSRepo : ICoursSGBSRepo
    {
        private readonly string _connectionString = @"Server=L575\MSSQL2025;Database=CoursSGBD;User ID=sa;Password=Ephec+2025;TrustServerCertificate=True;";
        private readonly ILogger<CoursSGBSRepo> _logger;
        public CoursSGBSRepo(ILogger<CoursSGBSRepo> logger)
        {
            _logger = logger;
        }

        public List<Student> GetAll()
        {
            List<Student> list = new List<Student>();
            
            _logger.LogInformation("Connecting to database with connection string: {ConnectionString}", _connectionString);

            Student student = new Student();
            student.Matricule = "A001";
            student.FirstName = "John";
            student.LastName = "Doe";
            list.Add(student);

            Student student2 = new Student();
            student2.Matricule = "A002";
            student2.FirstName = "Jane";
            student2.LastName = "Smith";
            list.Add(student2);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
            }

            return list;
        }

        public void Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "insert into dbo.Etudiant (ETU_NOM, ETU_PRENOM, ETU_MATRICULE) values ( @Nom , @Prenom , @Matricule)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nom", student.LastName);
                    command.Parameters.AddWithValue("@Prenom", student.FirstName);
                    command.Parameters.AddWithValue("@Matricule", student.Matricule);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("{RowsAffected} row(s) inserted.", rowsAffected);
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "delete from dbo.Etudiant where ETU_ID = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("{RowsAffected} row(s) deleted.", rowsAffected);
                }
            }
        }
    }
}
