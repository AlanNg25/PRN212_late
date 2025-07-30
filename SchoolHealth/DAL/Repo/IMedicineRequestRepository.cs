using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IMedicineRequestRepository
    {
        void Add(MedicineSent request);
        IEnumerable<MedicineSent> GetAll();
        IEnumerable<MedicineSent> GetByStudentId(int studentId);
    }
}
