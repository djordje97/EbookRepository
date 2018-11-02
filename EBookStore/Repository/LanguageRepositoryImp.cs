using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public class LanguageRepositoryImp:Repository<Language>,LanguageRepository
    {
        private AppDbContext context;
        public LanguageRepositoryImp(AppDbContext dbContext) : base(dbContext)
        {
            
        }

      
    }
}
