using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class MedicalEventRepo
    {
        private readonly StudentHealthManagementContext _context;

        public MedicalEventRepo()
        {
            _context = new StudentHealthManagementContext();
        }

        public List<MedicalEvent> GetMedicalEvents()
        {
            return _context.MedicalEvents.Include(me => me.Student)
                .ToList();
        }
    }
}
