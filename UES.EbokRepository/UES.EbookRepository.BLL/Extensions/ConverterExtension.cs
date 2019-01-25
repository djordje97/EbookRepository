using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.BLL.Extensions
{
   public static class ConverterExtension
    {
        #region[Category]
        internal static CategoryDTO ToDTO(this Category entity)
        {
            return new CategoryDTO()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name
            };
        }

        internal static Category ToEntity(this CategoryDTO dto)
        {
            return new Category()
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name
            };
        }

        internal static IEnumerable<CategoryDTO> ToDTOs(this IEnumerable<Category> entities)
        {
            return entities.Select(e => e.ToDTO());
        }

        internal static IEnumerable<Category> ToEntities(this IEnumerable<CategoryDTO> dtos)
        {
            return dtos.Select(d => d.ToEntity());
        }
        #endregion

        #region[Language]
        internal static LanguageDTO ToDTO(this Language entity)
        {
            return new LanguageDTO()
            {
                LanguageId = entity.LanguageId,
                Name = entity.Name
            };
        }

        internal static Language ToEntity(this LanguageDTO dto)
        {
            return new Language()
            {
                LanguageId = dto.LanguageId,
                Name = dto.Name
            };
        }

        internal static IEnumerable<LanguageDTO> ToDTOs(this IEnumerable<Language> entities)
        {
            return entities.Select(e => e.ToDTO());
        }

        internal static IEnumerable<Language> ToEntities(this IEnumerable<LanguageDTO> dtos)
        {
            return dtos.Select(d => d.ToEntity());
        }
        #endregion

        #region[User]
        internal static UserDTO ToDTO(this User entity)
        {
            if (entity == null)
                return null;
            return new UserDTO()
            {
                UserId = entity.UserId,
                Firstname = entity.Firstname,
                Lastname=entity.Lastname,
                Username=entity.Username,
                Password=entity.Password,
                Type=entity.Type,
                CategoryId=entity.CategoryId
            };
        }

        internal static User ToEntity(this UserDTO dto)
        {
            return new User()
            {
                UserId = dto.UserId,
                Firstname = dto.Firstname,
                Lastname=dto.Lastname,
                Username=dto.Username,
                Password=dto.Password,
                Type=dto.Type,
                CategoryId=dto.CategoryId
            };
        }

        internal static IEnumerable<UserDTO> ToDTOs(this IEnumerable<User> entities)
        {
            return entities.Select(e => e.ToDTO());
        }

        internal static IEnumerable<User> ToEntities(this IEnumerable<UserDTO> dtos)
        {
            return dtos.Select(d => d.ToEntity());
        }
        #endregion

        #region[Ebook]
        internal static EbookDTO ToDTO(this Ebook entity)
        {
            return new EbookDTO()
            {
                EbookId = entity.EbookId,
                Title = entity.Title,
                Author=entity.Author,
                Keywords=entity.Keywords,
                Filename=entity.Filename,
                MIME=entity.MIME,
                PublicationYear=entity.PublicationYear,
                CategoryId=entity.CategoryId,
                LanguageId=entity.LanguageId,
                UserId=entity.UserId
            };
        }

        internal static Ebook ToEntity(this EbookDTO dto)
        {
            return new Ebook()
            {
                EbookId = dto.EbookId,
                Title = dto.Title,
                Author = dto.Author,
                Keywords = dto.Keywords,
                Filename = dto.Filename,
                MIME = dto.MIME,
                PublicationYear = dto.PublicationYear,
                CategoryId = dto.CategoryId,
                LanguageId = dto.LanguageId,
                UserId = dto.UserId
            };
        }

        internal static IEnumerable<EbookDTO> ToDTOs(this IEnumerable<Ebook> entities)
        {
            return entities.Select(e => e.ToDTO());
        }

        internal static IEnumerable<Ebook> ToEntities(this IEnumerable<EbookDTO> dtos)
        {
            return dtos.Select(d => d.ToEntity());
        }
        #endregion
    }
}
