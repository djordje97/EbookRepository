using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.BLL.Contract.Contracts
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryDTO> GetAll();


        CategoryDTO GetById(int id);



        CategoryDTO Create(CategoryDTO dto);


        CategoryDTO Update(CategoryDTO dto);


        void Delete(int id);
    }
}
