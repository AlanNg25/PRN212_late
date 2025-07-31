using DAL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repo
{
    public class HealthCheckRepo
    {
        public bool Add(HealthCheck healthCheck)
        {
            try
            {
                using (var context = new StudentHealthManagementContext())
                {
                    context.HealthChecks.Add(healthCheck);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public HealthCheck? GetById(int checkId)
        {
            using (var context = new StudentHealthManagementContext())
            {
                return context.HealthChecks.FirstOrDefault(h => h.CheckId == checkId);
            }
        }

        public List<HealthCheck> GetAll()
        {
            using (var context = new StudentHealthManagementContext())
            {
                return context.HealthChecks.ToList();
            }
        }

        public bool Update(HealthCheck healthCheck)
        {
            try
            {
                using (var context = new StudentHealthManagementContext())
                {
                    context.HealthChecks.Update(healthCheck);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int checkId)
        {
            try
            {
                using (var context = new StudentHealthManagementContext())
                {
                    var healthCheck = context.HealthChecks.FirstOrDefault(h => h.CheckId == checkId);
                    if (healthCheck == null) return false;
                    context.HealthChecks.Remove(healthCheck);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
