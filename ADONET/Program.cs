// See https://aka.ms/new-console-template for more information

using ADONET.Models;
using ADONET.Services;
using Microsoft.Extensions.Logging;

var loggerFactory = LoggerFactory.Create(builder =>
{
builder.AddConsole();
builder.SetMinimumLevel(LogLevel.Debug);
});
ILogger logger = loggerFactory.CreateLogger<Program>();


try
{
    ILogger<StudentsService> studentsLogger = loggerFactory.CreateLogger<StudentsService>();
    StudentsService studentsService = new StudentsService(studentsLogger);


    logger.LogDebug("Fetching all students...");
    List<Student> students = studentsService.GetAll();

    foreach (var student in students)
    {
        logger.LogInformation("{Matricule} - {FirstName} {LastName} - {Email}", student.Matricule, student.FirstName, student.LastName, student.Email);
    }

    Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred");
}
