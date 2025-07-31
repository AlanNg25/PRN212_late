using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class HealthCheckFormRepository : IHealthCheckFormRepository
    {
        private readonly StudentHealthManagementContext _context;

        public HealthCheckFormRepository(StudentHealthManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<HealthCheckForm> GetUnconfirmedFormsByParentId(int parentId)
        {
            
            var studentIds = _context.Students
                .Where(s => s.ParentId == parentId)
                .Select(s => s.StudentId)
                .ToList();

            
            return _context.HealthCheckForms
                .Where(f => studentIds.Contains(f.StudentId) && (f.Confirmed == false || f.Confirmed == null))
                .ToList();
        }

        public bool ConfirmForm(int formId)
        {
            
            var form = _context.HealthCheckForms
                .FirstOrDefault(f => f.FormId == formId);

            if (form == null)
                return false;

            form.Confirmed = true;
            _context.SaveChanges();

            return true;
        }
    }

}
