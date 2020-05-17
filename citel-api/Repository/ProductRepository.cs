using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using citelapi.Models.Context;
using System.Linq;
using System.Threading.Tasks;
using citelapi.Models;

namespace citelapi.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CitelDbContext context): base(context) { }

        public async Task<IEnumerable<Product>> GetAllWithCategory()
        {
            return await entities.Include(i => i.Category).ToListAsync();
        }
    }
}