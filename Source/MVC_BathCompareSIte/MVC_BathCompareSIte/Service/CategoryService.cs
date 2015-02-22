using System;
using MVC_BathCompareSIte.DTO;

namespace MVC_BathCompareSIte.Service
{
    public interface CategoryService
    {
        CategoryListDTO GetAll();
        CategoryDTO Add(CategoryDTO dto);
        CategoryDTO Edit(CategoryDTO dto);
        CategoryDTO Delete(CategoryDTO dto);

        CategoryDTO GetByID(int id);
        bool HasChild(Int32 id);
    }
}