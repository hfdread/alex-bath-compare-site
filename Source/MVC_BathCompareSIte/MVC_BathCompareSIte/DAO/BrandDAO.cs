using System;
using System.Collections.Generic;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public interface BrandDao
    {
        IList<Brand> GetAll();
        int Add(BrandDTO dto);
        int Edit(BrandDTO dto);
        int Delete(BrandDTO dto);
        Brand GetById(Int32 id);
        Brand GetByName(string name);
    }
}