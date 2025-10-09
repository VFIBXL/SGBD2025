using ADONET.Interfaces;
using ADONET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Repositories
{
  
    public class StudentRepo : BaseRepo, IStudentRepo
    {
        private readonly string _connectionString = @"Server=L575\MSSQL2025;Database=CoursSGBD;User ID=sa;Password=Ephec+2025;TrustServerCertificate=True;";
        private readonly ILogger<StudentRepo> _logger;
        public StudentRepo(ILogger<StudentRepo> logger)
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
            string sql = GetFileFromAssemblyAsync("Etudiant_Insert.sql");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

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
            string sql = GetFileFromAssemblyAsync("Etudiant_delete.sql");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("{RowsAffected} row(s) deleted.", rowsAffected);
                }
            }
        }

        public void Update(Student student)
        {
            string sql = GetFileFromAssemblyAsync("Etudiant_update.sql");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Matricule", student.Matricule);
                    command.Parameters.AddWithValue("@Nom", student.LastName);
                    command.Parameters.AddWithValue("@Prenom", student.FirstName);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("{RowsAffected} row(s) updated.", rowsAffected);
                }
            }
        }

    }
}
