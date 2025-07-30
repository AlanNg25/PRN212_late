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

        public List<Blog> GetAllBlogs() => _repository.GetAllBlogs();

    }
}
