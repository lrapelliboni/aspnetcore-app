using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using citelapi.Models;

namespace citelapi.Repository
{
    public interface IProductRepository : IRepository<Product> {
        Task<IEnumerable<Product>> GetAllWithCategory();
    }  
}