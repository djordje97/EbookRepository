using EBookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public class UserRepositoryImp:Repository<User>,UserRepository
    {
        private AppDbContext context;
        public UserRepositoryImp(AppDbContext dbContext):base(dbContext)
        {
            context = dbContext;
        }

        public User GetByUsername(string username)
        {
            return context.Users.Where(x => x.Username == username).SingleOrDefault();
        }
    }
}
