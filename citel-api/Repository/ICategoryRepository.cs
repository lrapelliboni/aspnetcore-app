using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using citelapi.Models;

namespace citelapi.Repository
{
    public interface ICategoryRepository : IRepository<Category> {  
        Task<Category> GetByIdWithProducts(int id); 
    }  
}