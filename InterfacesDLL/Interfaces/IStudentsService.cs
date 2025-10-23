using Models;

namespace Interfaces
{
    public interface IStudentsService
    {
        List<Student> GetAll();
        void Add(Student student);
        void Remove(int id);
        void Update(Student student);

        List<Student> FindStudentsByLastName(string lastName);
    }
}
