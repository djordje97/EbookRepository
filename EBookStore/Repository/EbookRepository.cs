using EBookStore.Dto;
using EBookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public interface EbookRepository:IRepository<Ebook>
    {
        List<Ebook> GetEbooksByCategory(int categoryId);
        List<Ebook>Search(SearchModel searchModel);
    }
}
