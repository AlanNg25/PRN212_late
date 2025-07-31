using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AccountService
    {
        private readonly AccountRepo _repository;

        public AccountService()
        {
            _repository = new AccountRepo();
        }

        public object Login(string userName, string password) => _repository.Login(userName, password);

        public List<UserAccount> GetAllUsers() => _repository.GetAllUsers();

        public List<Student> GetStudents() => _repository.GetStudents();

        public List<Parent> GetParents() => _repository.GetParents();
    }
}
