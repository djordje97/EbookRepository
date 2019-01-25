using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Contract.Contracts
{
   public  interface IUserProvider
    {
        IEnumerable<User> GetAll();


        User GetById(int id);

        int Create(User user);

        int Update(User user);


        int Delete(int id);
    }
}
