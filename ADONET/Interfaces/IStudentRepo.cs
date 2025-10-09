using ADONET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Interfaces
{
    public interface IStudentRepo
    {
        List<Models.Student> GetAll();
        void Add(Student student);
        void Delete(int id);
        void Update(Student student);
    }
}
