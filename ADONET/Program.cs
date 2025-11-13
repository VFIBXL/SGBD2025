// See https://aka.ms/new-console-template for more information

using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using ModelsDLL.Profiles;
using Repositories;
using Services;
using ServicesDLL.Services;
using System.ComponentModel;

//var loggerFactory = LoggerFactory.Create(builder =>
//{
//builder.AddConsole();
//builder.SetMinimumLevel(LogLevel.Debug);
//});
//ILogger logger = loggerFactory.CreateLogger<Program>();


//try
//{
//    ILogger<StudentsService> studentsLogger = loggerFactory.CreateLogger<StudentsService>();
//    StudentsService studentsService = new StudentsService(studentsLogger);


//    logger.LogDebug("Fetching all students...");
//    List<Student> students = studentsService.GetAll();

//    foreach (var student in students)
//    {
//        logger.LogInformation("{Matricule} - {FirstName} {LastName} - {Email}", student.Matricule, student.FirstName, student.LastName, student.Email);
//    }

//    Console.WriteLine("Hello, World!");
//}
//catch (Exception ex)
//{
//    logger.LogError(ex, "An error occurred");
//}
namespace ADONET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var serviceProvider = ConfigureServices();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();
            var kotsService = serviceProvider.GetRequiredService<IKotService>();

            Console.WriteLine("Select an action:");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Delete");
            Console.WriteLine("3 - Add and check Matricule");
            Console.WriteLine("4 - Update Student");
            Console.WriteLine("5 - Get All Students");
            Console.WriteLine("6 - Find Students by Last Name");
            Console.Write("Your choice: ");
            var input = Console.ReadLine();

            int choice;
            if (!int.TryParse(input, out choice))
            {
                logger.LogWarning("Invalid input. Please enter a number.");
                return;
            }

            try
            {
                switch(choice)
                {
                    case 1:
                        Add(studentsService);
                        break;
                    case 2:
                        Delete(studentsService);
                        break;
                    case 3:
                        AddChechMatricule(studentsService);
                        break;
                    case 4:
                        UpdateStudent(studentsService);
                        break;
                    case 5:
                        GetAllStudent(studentsService);
                        break;
                    case 6:
                        FindStudentsByLastName(studentsService);
                        break;
                    case 7:
                        kotsService.GetAll();
                        break;

                    default:
                        logger.LogWarning("Invalid choice. Please select 1 to Add or 2 to Delete.");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing students.");
            }
        }

        private static void FindStudentsByLastName(IStudentsService studentsService)
        {
            Console.Write("Enter the last name to search: ");
            var lastName = Console.ReadLine();

            if (string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("LastName cannot be empty.");
                return;
            }

            try
            {
                var students = studentsService.FindStudentsByLastName(lastName);
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.Id} - {student.Matricule} - {student.FirstName} {student.LastName}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetAllStudent(IStudentsService studentsService)
        {
            var students = studentsService.GetAll();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} - {student.Matricule} - {student.FirstName} {student.LastName}");
            }
        }

        private static void UpdateStudent(IStudentsService studentsService)
        {
            Console.Write("Enter the ID of the student to delete: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                Student studentToUpdate = new Student();
                studentToUpdate.Id = id;
                studentToUpdate.Matricule = "PS003";
                studentToUpdate.FirstName = "UpdatedName" + id.ToString();
                studentToUpdate.LastName = "UpdatedLastName" + id.ToString();
                studentsService.Update(studentToUpdate);
            }
            else
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
            }
        }

        private static void Delete(IStudentsService studentsService)
        {
            studentsService.Remove(5);
        }

        private static void AddChechMatricule(IStudentsService studentsService)
        {
            Student newStudent = new Student();
            newStudent.Matricule = "X004";
            newStudent.FirstName = "Alice4";
            newStudent.LastName = "Johnson4";
            studentsService.Add(newStudent);
        }

        private static void Add(IStudentsService studentsService)
        {
            Student newStudent = new Student();
            newStudent.Matricule = "HE004";
            newStudent.FirstName = "Alice4";
            newStudent.LastName = "Johnson4";
            studentsService.Add(newStudent);

            newStudent.Matricule = "HE005";
            newStudent.FirstName = "Alice5";
            newStudent.LastName = "Johnson5";
            studentsService.Add(newStudent);

            newStudent.Matricule = "HE005";
            newStudent.FirstName = "Alice6";
            newStudent.LastName = "Johnson6";
            studentsService.Add(newStudent);

        }
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(cfg => { }, typeof(KotProfile));

            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<IStudentRepo, StudentRepo>()
                    .AddSingleton<IKotRepo, KotRepo>()
                    .AddSingleton<IKotService, KotService>()
                    .AddSingleton<IStudentsService, StudentsService>();

            return services.BuildServiceProvider();
        }
    }
}