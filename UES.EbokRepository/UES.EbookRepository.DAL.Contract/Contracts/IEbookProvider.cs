using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Contract.Contracts
{
   public interface IEbookProvider
    {
        IEnumerable<Ebook> GetAll();


        Ebook GetById(int id);

        int Create(Ebook ebook);

        int Update(Ebook ebook);


        int Delete(int id);
    }
}
