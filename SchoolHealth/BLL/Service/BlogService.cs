using DAL.Entities;
using DAL.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class BlogService
    {
        private readonly BlogRepo _repository;

        public BlogService()
        {
            _repository = new BlogRepo();
        }

        public void CreateBlog(Blog blog) => _repository.CreateBlog(blog);

        public void DeleteBlog(int blogId) => _repository.DeleteBlog(blogId);

        public List<Blog> GetAllBlogs() => _repository.GetAllBlogs();

        public void UpdateBlog(Blog blog) => _repository.UpdateBlog(blog);
    }
}
