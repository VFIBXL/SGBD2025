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
        private IStudentRepo _coursSGBSRepo;
        private readonly ILogger<StudentsService> _logger;
        public StudentsService(ILogger<StudentsService> logger, IStudentRepo coursSGBSRepo) 
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

        public List<Student> FindStudentsByLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("LastName cannot be null or empty");
            }
            List<Student> students = _coursSGBSRepo.FindStudentsByLastName(lastName);
            return students;
        }

        public void Add(Student student)
        {
            CheckMatricule(student.Matricule);
            CheckLastName(student.FirstName);

            _coursSGBSRepo.Add(student);
        }

        public void Update(Student student)
        {
            CheckMatricule(student.Matricule);
            CheckLastName(student.LastName);

            _coursSGBSRepo.Update(student);
        }   

        public void Remove(int id)
        {
            _coursSGBSRepo.Delete(id);
        }

        private void CheckMatricule(string matricule)
        {
            if (string.IsNullOrEmpty(matricule))
            {
                throw new ArgumentException("Matricule cannot be null or empty");
            }

            string prefix = matricule.Substring(0, 2).ToUpper();

            if (prefix != "HE" && prefix != "PS" )
            {
                throw new ArgumentException("Matricule must start with HE or PS");
            }
        }

        private void CheckLastName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("LastName cannot be null or empty");
            }
        }
    }
}
