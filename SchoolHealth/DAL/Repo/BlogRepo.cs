using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteBlog(int blogId)
        {
            try
            {
                var blog = _context.Blogs.Find(blogId);
                if (blog != null)
                {
                    _context.Blogs.Remove(blog);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Blog not found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error deleting blog", ex);
            }
        }

        public List<Blog> GetAllBlogs()
        {
            return _context.Blogs.Include(blg => blg.Author)
                .ToList();
        }
    }
}
