// See https://aka.ms/new-console-template for more information

using ADONET.Interfaces;
using ADONET.Models;
using ADONET.Repositories;
using ADONET.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            logger.LogInformation("Fetching all students...");
            studentsService.GetAll();
            logger.LogInformation("Students fetched successfully.");
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