using Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Moq;
using Services;

namespace Tests.Services
{
    public class StudentServiceTests
    {
        Mock<IStudentRepo> _repoMock;
        NullLogger<StudentsService> _logger;
        IStudentsService _service;

        public StudentServiceTests() 
        {
            _repoMock = new Mock<IStudentRepo>();
            _logger = NullLogger<StudentsService>.Instance;
            _service = new StudentsService(_logger, _repoMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Matricule = "HE001", FirstName = "John", LastName = "Doe" },
                new Student { Id = 2, Matricule = "PS002", FirstName = "Jane", LastName = "Smith"}
            };

            _repoMock.Setup(r => r.GetAll()).Returns(students);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Equal(students.Count, result.Count);
            Assert.Equal(students[0].Matricule, result[0].Matricule);
            Assert.Equal(students[1].LastName, result[1].LastName);

            _repoMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnsNoStudents()
        {
            // Arrange
            var students = new List<Student>();

            _repoMock.Setup(r => r.GetAll()).Returns(students);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Equal(students.Count, result.Count);

            _repoMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void AddWithValidData()
        {
            // Arrange
            var student = new Student
            {
                Matricule = "HE123",
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            _service.Add(student);

            // Assert
            _repoMock.Verify(r => r.Add(student), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("XX123")]
        public void AddWithInvalidMatriculeAndThrowsArgumentException(string? matricule)
        {
            var student = new Student
            {
                Matricule = matricule,
                FirstName = "John",
                LastName = "Doe"
            };

            Assert.Throws<ArgumentException>(() => _service.Add(student));
            _repoMock.Verify(r => r.Add(student), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddWithInvalidFirstNameAndThrowsArgumentException(string? firstName)
        {
            var student = new Student
            {
                Matricule = "HE123",
                FirstName = firstName,
                LastName = "lastName"
            };

            Assert.Throws<ArgumentException>(() => _service.Add(student));
            _repoMock.Verify(r => r.Add(student), Times.Never);
        }

        [Fact]
        public void AddWhenRepoThrowsExceptionPropagatesException()
        {
            // Arrange
            var student = new Student
            {
                Matricule = "HE123",
                FirstName = "John",
                LastName = "Doe"
            };

            // Simule une exception lors de l'appel à Add
            _repoMock.Setup(r => r.Add(It.IsAny<Student>())).Throws(new InvalidOperationException("DB error"));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _service.Add(student));
            Assert.Equal("DB error", ex.Message);

            _repoMock.Verify(r => r.Add(student), Times.Once);
        }
    }
}
