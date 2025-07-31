using DAL.Entities;
using DAL.Repo;
using System;

namespace BLL.Service
{
    public class HealthCheckService
    {
        private readonly HealthCheckRepo _healthCheckRepo;

        public HealthCheckService()
        {
            _healthCheckRepo = new HealthCheckRepo();
        }

        public bool SaveHealthCheck(HealthCheck healthCheck)
        {
            return _healthCheckRepo.Add(healthCheck);
        }

        // You can add more service methods that use _healthCheckRepo as needed.
    }
}
