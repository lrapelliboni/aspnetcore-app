using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using citelapi.Models.Context;
using System.Linq;
using System.Threading.Tasks;

namespace citelapi.Repository
{
   public class Repository <T> : IRepository <T> where T: class {  
        protected readonly CitelDbContext context;  
        protected DbSet <T> entities;  
        string errorMessage = string.Empty;  
        public Repository(CitelDbContext context) {  
            this.context = context;  
            entities = context.Set <T> ();  
        }  
        
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
      
        public Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
        
        public Task Remove(T entity)
        {
            entities.Remove(entity);
            return context.SaveChangesAsync();
        }
    }  
}