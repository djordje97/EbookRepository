using AutoMapper;
using EBookStore.Dto;
using EBookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Mappings
{
    public class SimpleMappings:Profile
    {
        public SimpleMappings()
        {
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, UserDto>().ReverseMap();
            CreateMap<Ebook, EbookDto>().ReverseMap();

        }
    }
}
