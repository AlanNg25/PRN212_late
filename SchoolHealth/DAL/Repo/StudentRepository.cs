using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repo
{
    public class StudentRepository
    {
        private readonly StudentHealthManagementContext _context;

        public StudentRepository()
        {
            _context = new StudentHealthManagementContext();
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == id);
        }

        
        public List<Student> GetStudentsByParentId(int parentId)
        {
            return _context.Students
                .Where(s => s.ParentId == parentId)
                .ToList();
        }
    }
}
