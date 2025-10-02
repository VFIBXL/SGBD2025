using ADONET.Interfaces;
using ADONET.Models;
using ADONET.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Services
{
    public class StudentsService : IStudentsService
    {
        private ICoursSGBSRepo _coursSGBSRepo;
        private readonly ILogger<StudentsService> _logger;
        public StudentsService(ILogger<StudentsService> logger, ICoursSGBSRepo coursSGBSRepo) 
        {
            _logger = logger;

            _coursSGBSRepo = coursSGBSRepo;
        }
        public List<Student> GetAll()
        {
            _logger.LogInformation("Entering GetAll method in StudentsService");
            List<Student> students = _coursSGBSRepo.GetAll();
            _logger.LogInformation("Exiting GetAll method in StudentsService");
            return students; 
        }
    }
}
