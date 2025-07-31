using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IHealthCheckFormRepository
    {
        IEnumerable<HealthCheckForm> GetUnconfirmedFormsByParentId(int parentId);
        bool ConfirmForm(int formId);
    }

}
