using Dapper;
using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ModelsDLL.DTO;
using System.Data;

namespace Repositories
{
  
    public class KotRepo : BaseRepo, IKotRepo
    {
        private readonly string _connectionString = @"Server=L575\MSSQL2025;Database=CoursSGBD;User ID=sa;Password=Ephec+2025;TrustServerCertificate=True;";
        private readonly ILogger<KotRepo> _logger;
        public KotRepo(ILogger<KotRepo> logger)
        {
            _logger = logger;
        }

        public KotRepo(ILogger<KotRepo> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        //public List<Student> FindStudentsByLastName(string lastName)
        //{
        //    List<Student> list = new List<Student>();
        //    string sql = GetFileFromAssemblyAsync("Etudiant_FindByLastNameDapper.sql");

        //    Dictionary<string, object> dbArgs = new Dictionary<string, object>();
        //    dbArgs.Add("@lastName", lastName);

        //    using (IDbConnection connection = new SqlConnection(_connectionString))
        //    {
        //        list = connection.Query<Student>(sql, dbArgs).ToList();
        //    }
        //    return list;
        //}

        public List<KotStudentDTO> GetAll()
        {
            List<KotStudentDTO> kots = new List<KotStudentDTO>();
            string sql = GetFileFromAssemblyAsync("Kot_SelectAll.sql");
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                kots = connection.Query<KotStudentDTO>(sql).ToList();
            }
            return kots;
        }

        //public void Add(Student student)
        //{
        //    string sql = GetFileFromAssemblyAsync("Etudiant_Insert.sql");

        //    Dictionary<string, object> dbArgs = new Dictionary<string, object>();
        //    dbArgs.Add("@Nom", student.LastName);
        //    dbArgs.Add("@Prenom", student.FirstName);
        //    dbArgs.Add("@Matricule", student.Matricule);

        //    using (IDbConnection connection = new SqlConnection(_connectionString))
        //    {
        //        int rowsAffected = connection.Execute(sql, dbArgs);
        //        _logger.LogInformation("{RowsAffected} row(s) inserted.", rowsAffected);
        //    }
        //}

        public void Delete(int id)
        {
            //string sql = GetFileFromAssemblyAsync("Etudiant_delete.sql");

            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", id);
            //        int rowsAffected = command.ExecuteNonQuery();
            //        _logger.LogInformation("{RowsAffected} row(s) deleted.", rowsAffected);
            //    }
            //}
            throw new NotImplementedException();
        }

        //public void Update(Student student)
        //{
            //string sql = GetFileFromAssemblyAsync("Etudiant_update.sql");

            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", student.Id);
            //        command.Parameters.AddWithValue("@Matricule", student.Matricule);
            //        command.Parameters.AddWithValue("@Nom", student.LastName);
            //        command.Parameters.AddWithValue("@Prenom", student.FirstName);
            //        int rowsAffected = command.ExecuteNonQuery();
            //        _logger.LogInformation("{RowsAffected} row(s) updated.", rowsAffected);
            //    }
            //}
            //throw new NotImplementedException();
        //}

    }
}
