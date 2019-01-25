using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;
using UES.EbookRepository.DAL.Contract.Contracts;

namespace UES.EbookRepository.BLL.Managers
{
    public class CategoryManager:ICategoryManager
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly IEbookManager _ebookManager;

        public CategoryManager(ICategoryProvider categoryProvider,IEbookManager ebookManager)
        {
            _categoryProvider = categoryProvider;
            _ebookManager = ebookManager;

        }


        public IEnumerable<CategoryDTO> GetAll()
        {
            return ConverterExtension.ToDTOs(_categoryProvider.GetAll());
        }

        public CategoryDTO GetById(int id)
        {
            return ConverterExtension.ToDTO(_categoryProvider.GetById(id));
        }


        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            var category = ConverterExtension.ToEntity(categoryDTO);
            int newCategoryId = 0;
            newCategoryId = _categoryProvider.Create(category);
            return ConverterExtension.ToDTO(_categoryProvider.GetById(newCategoryId));
        }

        public CategoryDTO Update(CategoryDTO categoryDTO)
        {
            var category = ConverterExtension.ToEntity(categoryDTO);
            int categoryId;
            categoryId = _categoryProvider.Update(category);
            return ConverterExtension.ToDTO(_categoryProvider.GetById(categoryId));
        }

        public void Delete(int id)
        {
            var books = _ebookManager.GetByCategory(id);
            foreach (var book in books)
            {
                _ebookManager.Delete(book.EbookId);
            }
            _categoryProvider.Delete(id);
        }
    }
}
