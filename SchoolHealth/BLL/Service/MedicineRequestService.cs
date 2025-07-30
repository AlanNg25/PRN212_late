using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repo;
using System.Collections.Generic;

public class MedicineRequestService
{
    private readonly IMedicineRequestRepository _repository;

    public MedicineRequestService(IMedicineRequestRepository repository)
    {
        _repository = repository;
    }

    public void SendRequest(MedicineSent request)
    {
        _repository.Add(request);
    }

    public IEnumerable<MedicineSent> GetRequestsByStudent(int studentId)
    {
        return _repository.GetByStudentId(studentId);
    }
}
