using EBookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Repository
{
    public interface LanguageRepository:IRepository<Language>
    {
        Language GetByName(string name);
    }
}
