using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public class EbookRepositoryImp:Repository<Ebook>,EbookRepository
    {
        private AppDbContext context;
        public EbookRepositoryImp(AppDbContext dbContext):base(dbContext)
        {
            context = dbContext;
        }

        public List<Ebook> GetEbooksByCategory(int categoryId)
        {
            return context.Ebooks.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
