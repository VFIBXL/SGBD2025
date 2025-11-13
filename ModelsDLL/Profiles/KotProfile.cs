using AutoMapper;
using Models;
using ModelsDLL.DTO;
using ModelsDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModelsDLL.Profiles
{
    public class KotProfile : Profile
    {
        public KotProfile()
        {
            CreateMap<KotStudentDTO, Student>()
                .ForMember(d => d.Matricule, opt => opt.MapFrom(s => s.ETU_MATRICULE))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.ETU_NOM))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.ETU_PRENOM));

            CreateMap<KotStudentDTO, Kot>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.KOT_ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.KOT_NAME))
                .ForMember(d => d.Resident, opt => opt.MapFrom(s => s));
        }
    }
}
