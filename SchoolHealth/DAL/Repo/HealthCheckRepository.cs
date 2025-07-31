using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

public class HealthCheckRepository : IHealthCheckRepository
{
    private readonly StudentHealthManagementContext _context;

    public HealthCheckRepository()
    {
        _context = new StudentHealthManagementContext();
    }

    public IEnumerable<HealthCheck> GetByStudentId(int studentId)
    {
        return _context.HealthChecks.Where(h => h.StudentId == studentId).ToList();
    }

    public void Add(HealthCheck healthCheck)
    {
        _context.HealthChecks.Add(healthCheck);
        _context.SaveChanges();
    }
}

