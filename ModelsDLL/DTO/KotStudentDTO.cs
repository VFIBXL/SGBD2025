using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDLL.DTO
{
    public record KotStudentDTO (int KOT_ID, string KOT_NAME, string ETU_MATRICULE, string ETU_NOM, string ETU_PRENOM);
    
}
