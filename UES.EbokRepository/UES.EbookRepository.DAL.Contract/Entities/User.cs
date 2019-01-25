using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.DAL.Contract.Entities
{
   public class User
    {
      
        public int UserId { get; set; }
       
        public string Firstname { get; set; }
    
        public string Lastname { get; set; }
       
        public string Username { get; set; }
       
        public string Password { get; set; }
        public string Type { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
