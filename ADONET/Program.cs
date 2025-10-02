// See https://aka.ms/new-console-template for more information

using ADONET.Interfaces;
using ADONET.Models;
using ADONET.Repositories;
using ADONET.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            int choice = Convert.ToInt32(args[0]);

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
                    default:
                        logger.LogWarning("Invalid choice. Please select 1 to Add or 2 to Delete.");
                        break;
                }


                //logger.LogInformation("Fetching all students...");
                //studentsService.GetAll();
                //logger.LogInformation("Students fetched successfully.");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing students.");
            }
        }

        private static void Delete(IStudentsService studentsService)
        {
            studentsService.Remove(5);
        }

        private static void Add(IStudentsService studentsService)
        {
            Student newStudent = new Student();
            newStudent.Matricule = "A004";
            newStudent.FirstName = "Alice4";
            newStudent.LastName = "Johnson4";
            studentsService.Add(newStudent);

            newStudent.Matricule = "A005";
            newStudent.FirstName = "Alice5";
            newStudent.LastName = "Johnson5";
            studentsService.Add(newStudent);

            newStudent.Matricule = "A005";
            newStudent.FirstName = "Alice6";
            newStudent.LastName = "Johnson6";
            studentsService.Add(newStudent);

        }
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            
            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<ICoursSGBSRepo, CoursSGBSRepo>()
                    .AddSingleton<IStudentsService, StudentsService>();

            return services.BuildServiceProvider();
        }
    }
}