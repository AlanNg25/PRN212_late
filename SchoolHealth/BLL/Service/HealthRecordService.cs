using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class HealthRecordService
    {
        private readonly IHealthRecordRepository _repository;

        public HealthRecordService(IHealthRecordRepository repository)
        {
            _repository = repository;
        }

        public void CreateHealthRecord(HealthRecord record)
        {
            // Add validation as needed
            _repository.Add(record);
        }

        public IEnumerable<HealthRecord> GetAllRecords() => _repository.GetAll();
    }

}
