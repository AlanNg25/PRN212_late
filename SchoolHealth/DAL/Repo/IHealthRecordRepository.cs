using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IHealthRecordRepository
    {
        void Add(HealthRecord record);
        IEnumerable<HealthRecord> GetAll();
    }

}
