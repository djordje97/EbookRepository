using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Contract.Contracts
{
    public interface ICategoryProvider
    {
        IEnumerable<Category> GetAll();


        Category GetById(int id);

        int Create(Category category);

        int Update(Category category);


        int Delete(int id);
    }
}
