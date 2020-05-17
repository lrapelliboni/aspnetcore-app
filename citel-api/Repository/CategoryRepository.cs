using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using citelapi.Models.Context;
using System.Linq;
using System.Threading.Tasks;
using citelapi.Models;

namespace citelapi.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CitelDbContext context): base(context) { }

        public async Task<Category> GetByIdWithProducts(int id)
        {
            return await entities.Include(i=>i.Products).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}