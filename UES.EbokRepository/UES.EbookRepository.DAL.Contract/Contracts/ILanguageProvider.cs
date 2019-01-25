using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Contract.Contracts
{
   public interface ILanguageProvider
    {
        IEnumerable<Language> GetAll();


        Language GetById(int id);

        int Create(Language language);

        int Update(Language language);


        int Delete(int id);
    }
}
