using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
 public   interface IRepository<T> where T:class
    {
         List<T> GetAll();
        T GetOne(int id);

        T Save(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Complete();
    }
}
