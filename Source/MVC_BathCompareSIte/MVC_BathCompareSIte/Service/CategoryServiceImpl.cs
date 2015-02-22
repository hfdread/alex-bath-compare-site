using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.DTO;

namespace MVC_BathCompareSIte.Service
{
    public class CategoryServiceImpl : CategoryService
    {
        private CategoryDao _dao = new CategoryDaoImpl();

        public CategoryDao Dao
        {
            get { return this._dao; }
            set { this._dao = value; }
        }
        public CategoryListDTO GetAll()
        {
            var resultList = new CategoryListDTO {DtoList = new List<CategoryDTO>(), ErrorList = new List<string>()};

            try
            {
                var tempList = _dao.GetAll();
                resultList.DtoList = tempList.Select(q => new CategoryDTO
                {
                   Description = q.Description,
                   Id = q.Id,
                   Name = q.Name,
                   ParentCategoryId =q.ParentId.ToString(),
                }).ToList();

                foreach (var item in resultList.DtoList)
                {
                    item.Parent = item.ParentCategoryId == "0" ? "" : _dao.GetById( Convert.ToInt32(item.ParentCategoryId)).Name;
                    item.selIndex = item.ParentCategoryId == "0" ? -1 : Convert.ToInt32(item.ParentCategoryId);
                }
            }
            catch (Exception e)
            {
                resultList = new CategoryListDTO {ErrorList = new List<string> {e.Message}};
            }

            return resultList;
        }

        public CategoryDTO Add(CategoryDTO dto)
        {
            var result = new CategoryDTO {ErrorList = new List<string>()};
            try
            {
                int nResult = _dao.Add(dto);
                if (nResult <= 0)
                {
                    result.ErrorList.Add("Add Failed!");
                }
                else
                {
                    var temp = _dao.GetById(dto.Id);
                    result.Id = temp.Id;
                    result.Name = temp.Name;
                    result.ParentCategoryId = temp.ParentId.ToString();
                }
            }
            catch (Exception e)
            {
                result = new CategoryDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public CategoryDTO Edit(CategoryDTO dto)
        {
            var result = new CategoryDTO { ErrorList = new List<string>() };
            try
            {
                int nResult = _dao.Edit(dto);
                if (nResult <= 0)
                {
                    result.ErrorList.Add("Edit Failed!");
                }
                else
                {
                    var temp = _dao.GetById(dto.Id);
                    result.Id = temp.Id;
                    result.Name = temp.Name;
                    result.ParentCategoryId = temp.ParentId.ToString();
                }
            }
            catch (Exception e)
            {
                result = new CategoryDTO { ErrorList = new List<string> { e.Message } };
            }

            return result;
        }

        public CategoryDTO Delete(CategoryDTO dto)
        {
            var result = new CategoryDTO { ErrorList = new List<string>() };
            try
            {
                int nResult = _dao.Delete(dto);
                if (nResult <= 0)
                {
                    result.ErrorList.Add("Delete Failed!");
                }
            }
            catch (Exception e)
            {
                result = new CategoryDTO { ErrorList = new List<string> { e.Message } };
            }

            return result;
        }

        public CategoryDTO GetByID(int id)
        {
            CategoryDTO result = new CategoryDTO {ErrorList = new List<string>()};
            try
            {
                var temp = _dao.GetById(id);
                if (temp != null)
                {
                    result.Parent = temp.ParentId == 0 ? "" : _dao.GetById(temp.ParentId).Name;
                    result.selIndex = temp.ParentId == 0 ? -1 :  temp.ParentId;
                    result.Name = temp.Name;
                    result.Description = temp.Description;
                    result.Id = temp.Id;
                    result.ParentCategoryId = result.selIndex.ToString();
                }
            }
            catch (Exception e)
            {
                result = new CategoryDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public bool HasChild(int id)
        {
            bool bRet = false;
            try
            {
                var childList = GetAll().DtoList.Where(q => q.ParentCategoryId.Equals(id.ToString())).ToList();
                if (childList.Count > 0)
                {
                    bRet = true;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return bRet;
        }
    }
}