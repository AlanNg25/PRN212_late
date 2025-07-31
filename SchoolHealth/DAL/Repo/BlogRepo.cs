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

        public void CreateBlog(Blog blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ArgumentNullException(nameof(blog), "Blog cannot be null");
                }
                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating blog", ex);
            }
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

        public void UpdateBlog(Blog blog)
        {
            try
            {
                var existingBlog = _context.Blogs.Find(blog.BlogId);
                if (existingBlog == null)
                {
                    throw new Exception("Blog not found");
                }
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.DatePosted = blog.DatePosted;
                existingBlog.AuthorId = blog.AuthorId;
                existingBlog.Type = blog.Type;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating blog", ex);
            }
        }
    }
}
