using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.BLL.Contract.Contracts
{
   public interface ILanguageManager
    {
        IEnumerable<LanguageDTO> GetAll();


        LanguageDTO GetById(int id);

        LanguageDTO GetByName(string name);

        LanguageDTO Create(LanguageDTO dto);


        LanguageDTO Update(LanguageDTO dto);


        void Delete(int id);
    }
}
