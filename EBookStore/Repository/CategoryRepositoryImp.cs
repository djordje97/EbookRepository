using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public class CategoryRepositoryImp:Repository<Category>,CategoryRepository
    {
        private AppDbContext context;

        public CategoryRepositoryImp(AppDbContext dbContext):base(dbContext)
        {
        
        }
    }
}
