using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.BLL.Contract.Contracts
{
   public  interface IEbookManager
    {
        IEnumerable<EbookDTO> GetAll();


        EbookDTO GetById(int id);

        EbookDTO GetByFilename(string filename);

        IEnumerable<EbookDTO> GetByCategory(int categoryId);

        List<ResultData> Search(SearchModel searchModel);
        EbookDTO Create(IndexUnit unit);


        EbookDTO Update(IndexUnit indexUnit);


        void Delete(int id);
    }
}
