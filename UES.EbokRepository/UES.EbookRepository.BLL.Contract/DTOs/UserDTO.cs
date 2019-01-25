using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Contract.DTOs
{
   public class UserDTO
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set;}
        }
}
