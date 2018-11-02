using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
        private AppDbContext dbContext;

        public Repository(AppDbContext appDbContext)
        {
            dbContext = appDbContext;

        }

        public  void Delete(T entity)
        {
           dbContext.Set<T>().Remove(entity);
        }

        public  List<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public  T GetOne(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public  T Save(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }

        public  T Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public  void Complete()
        {
            dbContext.SaveChanges();
        }
    }
}
