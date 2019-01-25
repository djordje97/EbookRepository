using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.BLL.Contract.Contracts
{
   public interface IIndexManager
    {
        void Index(IndexUnit unit);
        IndexUnit GetIndexUnit(string filename);
        bool Delete(string filename);

    }
}
