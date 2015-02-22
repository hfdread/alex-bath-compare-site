using System;
using System.Collections.Generic;
using System.Linq;
using MVC_BathCompareSIte.Models;
using MVC_BathCompareSIte.DTO;

namespace MVC_BathCompareSIte.DAO
{
    public class BrandDaoImpl : BrandDao
    {
        private ItemDbContext _dbContext;
        public IList<Brand> GetAll()
        {
            IList<Brand> list;

            using (_dbContext = new ItemDbContext())
            {
                list = _dbContext.Brands.ToList();
            }
            
            return list;
        }

        public int Add(BrandDTO dto)
        {
            int nResult = 0;

            using (_dbContext = new ItemDbContext())
            {
                Brand model = new Brand();
                model.Name = dto.Name;
                model.Description = dto.Description;

                _dbContext.Brands.Add(model);

                nResult = _dbContext.SaveChanges();
            }
            return nResult;
        }

        public int Edit(BrandDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Brands.FirstOrDefault(q => q.Id.Equals(dto.Id));
                if (model != null)
                {
                    model.Name = dto.Name;
                    model.Description = dto.Description;
                }

                nResult = _dbContext.SaveChanges();
            }
            return nResult;
        }

        public int Delete(BrandDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Brands.FirstOrDefault(q => q.Id.Equals(dto.Id));
                if (model != null)
                {
                    _dbContext.Brands.Remove(model);
                    nResult = _dbContext.SaveChanges();
                }
            }

            return nResult;
        }

        public Brand GetById(int id)
        {
            Brand model;
            using (_dbContext = new ItemDbContext())
            {
                model = _dbContext.Brands.FirstOrDefault(q => q.Id.Equals(id));
            }

            return model;
        }

        public Brand GetByName(string name)
        {
            Brand model;
            using (_dbContext = new ItemDbContext())
            {
                model = _dbContext.Brands.FirstOrDefault(q => q.Name.ToLower().Equals(name.ToLower()));
            }

            return model;
        }
    }
}