using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public interface CategoryDao
    {
        IList<Category> GetAll();
        Category GetById(Int32 id);
        Category GetByName(string name);
        int Add(CategoryDTO dto);
        int Edit(CategoryDTO dto);
        int Delete(CategoryDTO dto);
    }
}