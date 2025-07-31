using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MedicalEventService
    {
        private readonly MedicalEventRepo _repository;
        public MedicalEventService()
        {
            _repository = new MedicalEventRepo();
        }
        public List<MedicalEvent> GetMedicalEvents() => _repository.GetMedicalEvents();
    }
}
