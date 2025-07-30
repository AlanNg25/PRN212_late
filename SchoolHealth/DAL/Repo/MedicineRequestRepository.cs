using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class MedicineRequestRepository : IMedicineRequestRepository
    {
        private readonly StudentHealthManagementContext _context;

        public MedicineRequestRepository(StudentHealthManagementContext context)
        {
            _context = context;
        }

        public void Add(MedicineSent request)
        {
            _context.MedicineSents.Add(request);
            _context.SaveChanges();
        }

     
        public IEnumerable<MedicineSent> GetAll()
        {
            return _context.MedicineSents.ToList();
        }

        
        public IEnumerable<MedicineSent> GetByStudentId(int studentId)
        {
            return _context.MedicineSents
                           .Where(r => r.StudentId == studentId)
                           .ToList();
        }
    }
    }
