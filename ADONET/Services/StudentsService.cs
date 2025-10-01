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
    public class StudentsService
    {
        private CoursSGBSRepo _coursSGBSRepo;
        private readonly ILogger<StudentsService> _logger;
        public StudentsService(ILogger<StudentsService> logger) 
        {
            _logger = logger;

            _coursSGBSRepo = new CoursSGBSRepo();
        }
        public List<Student> GetAll()
        {
            _logger.LogDebug("Entering GetAll method in StudentsService");
            List<Student> students = _coursSGBSRepo.GetAll();
            return students; 
        }
    }
}
