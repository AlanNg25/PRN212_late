using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using DAL.Repo;
using System.Collections.Generic;

public class HealthCheckService
{
    private readonly IHealthCheckRepository _repository;

    public HealthCheckService(IHealthCheckRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<HealthCheck> GetByStudentId(int studentId)
    {
        return _repository.GetByStudentId(studentId);
    }

    public void AddHealthCheck(HealthCheck healthCheck)
    {
        _repository.Add(healthCheck);
    }
}

