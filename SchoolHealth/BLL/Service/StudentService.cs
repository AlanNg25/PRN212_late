using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepo;

        public StudentService(StudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepo.GetAllStudents();
        }
        public List<Student> GetStudentsByParentId(int parentId)
        {
            return _studentRepo.GetStudentsByParentId(parentId);
        }

        public Student? GetStudentById(int id)
        {
            return _studentRepo.GetStudentById(id);
        }
        
    }
}
