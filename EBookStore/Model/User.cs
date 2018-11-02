using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Model
{
    public class User
    {

        public int UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(30)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(10)]
        public string Username { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
