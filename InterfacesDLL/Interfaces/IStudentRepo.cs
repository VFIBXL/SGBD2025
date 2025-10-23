using Models;

namespace Interfaces
{
    public interface IStudentRepo
    {
        List<Models.Student> GetAll();
        void Add(Student student);
        void Delete(int id);
        void Update(Student student);
        List<Student> FindStudentsByLastName(string lastName);
    }
}
