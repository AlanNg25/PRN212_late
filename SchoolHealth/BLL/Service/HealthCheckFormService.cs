using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class HealthCheckFormService
    {
        private readonly IHealthCheckFormRepository _repo;

        public HealthCheckFormService(IHealthCheckFormRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<HealthCheckForm> GetUnconfirmedFormsByParentId(int parentId)
        {
            return _repo.GetUnconfirmedFormsByParentId(parentId);
        }

        public bool ConfirmForm(int formId)
        {
            return _repo.ConfirmForm(formId);
        }
    }
}
