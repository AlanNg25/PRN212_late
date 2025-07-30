using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class BlogRepo
    {
        private readonly StudentHealthManagementContext _context;

        public BlogRepo()
        {
            _context = new StudentHealthManagementContext();
        }

        public List<Blog> GetAllBlogs()
        {
            return _context.Blogs.ToList();
        }
    }
}
