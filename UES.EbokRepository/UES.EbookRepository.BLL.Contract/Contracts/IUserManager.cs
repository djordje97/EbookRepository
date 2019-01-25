using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.BLL.Contract.Contracts
{
   public  interface IUserManager
    {
        IEnumerable<UserDTO> GetAll();


        UserDTO GetById(int id);

        UserDTO GetByUsername(string username);

        UserDTO Create(UserDTO dto);


        UserDTO Update(UserDTO dto);


        void Delete(int id);
    }
}
