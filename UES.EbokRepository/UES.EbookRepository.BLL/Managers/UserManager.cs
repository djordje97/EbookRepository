using System.Collections.Generic;
using System.Linq;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;
using UES.EbookRepository.DAL.Contract.Contracts;

namespace UES.EbookRepository.BLL.Managers
{
    public class UserManager:IUserManager
    {
        private readonly IUserProvider _userProvider;

        public UserManager(IUserProvider userProvider)
        {
            _userProvider = userProvider;

        }


        public IEnumerable<UserDTO> GetAll()
        {
            return ConverterExtension.ToDTOs(_userProvider.GetAll());
        }

        public UserDTO GetById(int id)
        {
            return ConverterExtension.ToDTO(_userProvider.GetById(id));
        }


        public UserDTO Create(UserDTO userDTO)
        {
            var user = ConverterExtension.ToEntity(userDTO);
            int newUserId = 0;
               newUserId = _userProvider.Create(user);
            return ConverterExtension.ToDTO(_userProvider.GetById(newUserId));
        }

        public UserDTO Update(UserDTO userDTO)
        {
            var user = ConverterExtension.ToEntity(userDTO);
            int userId;
            userId = _userProvider.Update(user);
            return ConverterExtension.ToDTO(_userProvider.GetById(userId));
        }

        public void Delete(int id)
        {
            _userProvider.Delete(id);
        }

        public UserDTO GetByUsername(string username)
        {
            var users = _userProvider.GetAll();
            return ConverterExtension.ToDTO(users.Where(user => user.Username == username).SingleOrDefault());
        }
    }
}
