using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class AccountRepo
    {
        private readonly StudentHealthManagementContext _context;

        public AccountRepo()
        {
            _context = new StudentHealthManagementContext();
        }

        public object Login(string text, string password)
        {
            // Assuming you have a User entity with properties Username and Password
            var user = _context.UserAccounts.Include(ua => ua.Parent)
                .FirstOrDefault(u => u.Username == text && u.PasswordHash == password);
            if (user != null)
            {
                return user;
            }
            return null; // or throw an exception, or return a specific error message
        }

        public List<UserAccount> GetAllUsers()
        {
            return _context.UserAccounts.Include(ua => ua.Parent).ToList();
        }

    }
}
