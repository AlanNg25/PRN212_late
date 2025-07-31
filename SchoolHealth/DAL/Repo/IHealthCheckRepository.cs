using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using System.Collections.Generic;

public interface IHealthCheckRepository
{
    IEnumerable<HealthCheck> GetByStudentId(int studentId);
    void Add(HealthCheck healthCheck);
}
