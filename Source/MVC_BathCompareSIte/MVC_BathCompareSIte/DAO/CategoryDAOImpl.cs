using System;
using System.Collections.Generic;
using System.Linq;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public class CategoryDaoImpl : CategoryDao
    {
        private ItemDbContext _dbContext = new ItemDbContext();

        public IList<Category> GetAll()
        {
            IList<Category> result = null;

            using (_dbContext = new ItemDbContext())
            {
                result = _dbContext.Categories.ToList();
            }

            return result;
        }

        public Category GetById(Int32 id)
        {
            Category result;

            using (_dbContext = new ItemDbContext())
            {
                result = _dbContext.Categories.FirstOrDefault(q => q.Id.Equals(id));
            }

            return result;
        }

        public Category GetByName(string name)
        {
            Category model;

            using (_dbContext = new ItemDbContext())
            {
                model = _dbContext.Categories.FirstOrDefault(q => q.Name.ToLower().Equals(name.ToLower()));
            }
            return model;
        }

        public int Add(CategoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                Category model = new Category();
                model.Description = dto.Description;
                model.Name = dto.Name;
                model.ParentId = Convert.ToInt32(dto.ParentCategoryId);

                _dbContext.Categories.Add(model);
                nResult = _dbContext.SaveChanges();
            }

            return nResult;
        }

        public int Edit(CategoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Categories.FirstOrDefault(q => q.Id.Equals(dto.Id));
                if (model != null)
                {
                    model.Name = dto.Name;
                    model.Description = dto.Description;
                    model.ParentId = Convert.ToInt32(dto.ParentCategoryId);
                }

                nResult = _dbContext.SaveChanges();
            }

            return nResult;
        }

        public int Delete(CategoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Categories.FirstOrDefault(q => q.Id.Equals(dto.Id));
                if (model != null)
                {
                    _dbContext.Categories.Remove(model);
                    nResult = _dbContext.SaveChanges();
                }
            }
            return nResult;
        }
    }
}