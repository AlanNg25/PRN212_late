using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly StudentHealthManagementContext _context;

        public HealthRecordRepository()
        {
            _context = new StudentHealthManagementContext();
        }

        public void Add(HealthRecord record)
        {
            _context.HealthRecords.Add(record);
            _context.SaveChanges(); 
        }

        public IEnumerable<HealthRecord> GetAll()
        {
            return _context.HealthRecords.ToList();
        }
    }
}
