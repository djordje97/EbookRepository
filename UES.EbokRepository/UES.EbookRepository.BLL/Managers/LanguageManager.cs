using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;
using UES.EbookRepository.DAL.Contract.Contracts;

namespace UES.EbookRepository.BLL.Managers
{
    public class LanguageManager:ILanguageManager
    {
        private readonly ILanguageProvider _languageProvider;

        public LanguageManager(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;

        }


        public IEnumerable<LanguageDTO> GetAll()
        {
            return ConverterExtension.ToDTOs(_languageProvider.GetAll());
        }

        public LanguageDTO GetById(int id)
        {
            return ConverterExtension.ToDTO(_languageProvider.GetById(id));
        }


        public LanguageDTO Create(LanguageDTO languageDTO)
        {
            var category = ConverterExtension.ToEntity(languageDTO);
            int newLanguageId = 0;
            newLanguageId = _languageProvider.Create(category);
            return ConverterExtension.ToDTO(_languageProvider.GetById(newLanguageId));
        }

        public LanguageDTO Update(LanguageDTO languageDTO)
        {
            var language = ConverterExtension.ToEntity(languageDTO);
            int languageId;
            languageId = _languageProvider.Update(language);
            return ConverterExtension.ToDTO(_languageProvider.GetById(languageId));
        }

        public void Delete(int id)
        {
            _languageProvider.Delete(id);
        }

        public LanguageDTO GetByName(string name)
        {
            var languages = _languageProvider.GetAll();

            return ConverterExtension.ToDTO(languages.Where(language => language.Name == name).SingleOrDefault());
        }
    }
}
