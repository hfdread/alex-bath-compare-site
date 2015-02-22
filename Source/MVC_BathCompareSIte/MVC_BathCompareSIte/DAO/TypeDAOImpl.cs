using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public class TypeDAOImpl : TypeDao
    {
        private ItemDbContext _dbContext = new ItemDbContext();

        public ProdType GetByName(string name)
        {
            ProdType model;

            using(_dbContext = new ItemDbContext())
            {
                model = _dbContext.Types.FirstOrDefault(q => q.Name.ToLower().Equals(name.ToLower()));
            }

            return model;
        }

        public int Add(TypeDTO dto)
        {
            int nRet = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = new ProdType();
                model.Name = dto.Name;
                model.Description = dto.Description;
                model.ParentId = dto.Parent;

                _dbContext.Types.Add(model);
                nRet = _dbContext.SaveChanges();
            }
            return nRet;
        }

        public int Edit(TypeDTO dto)
        {
            int nRet = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Types.FirstOrDefault(q => q.Id.Equals(dto.Id));

                if (model != null)
                {
                    model.Name = dto.Name;
                    model.Description = dto.Description;
                    model.ParentId = dto.Parent;
                    nRet = _dbContext.SaveChanges();
                }
            }
            return nRet;
        }

        public int Delete(TypeDTO dto)
        {
            int nRet = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Types.FirstOrDefault(q => q.Id.Equals(dto.Id));

                if (model != null)
                {
                    _dbContext.Types.Remove(model);
                    nRet = _dbContext.SaveChanges();
                }

            }

            return nRet;
        }
    }
}