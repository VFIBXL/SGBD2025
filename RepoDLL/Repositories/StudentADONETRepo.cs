using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Models;

namespace Repositories
{
  
    public class StudentADONETRepo : BaseRepo, IStudentRepo
    {
        private readonly string _connectionString = @"Server=L575\MSSQL2025;Database=CoursSGBD;User ID=sa;Password=Ephec+2025;TrustServerCertificate=True;";
        private readonly ILogger<StudentADONETRepo> _logger;
        public StudentADONETRepo(ILogger<StudentADONETRepo> logger)
        {
            _logger = logger;
        }

        public List<Student> FindStudentsByLastName(string lastName)
        {
            List<Student> list = new List<Student>();
            string sql = GetFileFromAssemblyAsync("Etudiant_FindByLastName.sql");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@lastName", lastName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                Id = Convert.ToInt16(reader["Etu_Id"]),
                                FirstName = reader["Etu_Prenom"] == DBNull.Value ? null : reader["Etu_Prenom"].ToString(),
                                Matricule = reader["Etu_Matricule"].ToString(),
                                LastName = reader["Etu_Nom"] == DBNull.Value ? "" : reader["Etu_Nom"].ToString()
                            };
                            list.Add(student);
                        }
                    }
                }
            }
            return list;
        }

        public List<Student> GetAll()
        {
            List<Student> list = new List<Student>();

            string sql = GetFileFromAssemblyAsync("Etudiant_SelectAll.sql");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            //Id = reader.GetInt32(reader.GetOrdinal("Etu_Id")),
                            //Matricule = reader.GetString(reader.GetOrdinal("Etu_Matricule")),
                            //FirstName = reader.IsDBNull(reader.GetOrdinal("Etu_Prenom")) ? null : reader.GetString(reader.GetOrdinal("Etu_Prenom")),
                            //LastName = reader.GetString(reader.GetOrdinal("Etu_Nom")),
                            Id = Convert.ToInt16(reader["Etu_Id"]),
                            FirstName = reader["Etu_Prenom"] == DBNull.Value ? null : reader["Etu_Prenom"].ToString(),
                            Matricule = reader["Etu_Matricule"].ToString(),
                            LastName = reader["Etu_Nom"] == DBNull.Value ? "" : reader["Etu_Nom"].ToString()
                        };
                        list.Add(student);
                    }
                }
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
