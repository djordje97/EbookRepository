using EBookStore.Dto;
using EBookStore.Lucene.Model;
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
        List<ResultData>Search(SearchModel searchModel);
    }
}
