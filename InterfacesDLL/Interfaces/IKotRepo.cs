using Models;
using ModelsDLL.DTO;
using ModelsDLL.Models;

namespace Interfaces
{
    public interface IKotRepo
    {
        List<KotStudentDTO> GetAll();
        void Delete(int id);
    }
}
