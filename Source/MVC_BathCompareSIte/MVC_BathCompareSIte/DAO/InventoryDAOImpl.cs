using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.Extensions;
using Microsoft.Ajax.Utilities;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public class InventoryDaoImpl : InventoryDao
    {
        private ItemDbContext _dbContext = new ItemDbContext();
        private IList<Int32> searchCateList = new List<Int32>();
        private CategoryDao cateDao = new CategoryDaoImpl();
        private IList<Category> sourceList = new List<Category>();

        public IList<Inventory> SearchInventories(SearchForm form)
        {
            IList<Inventory> result = null;

            Int32 brandId = string.IsNullOrWhiteSpace(form.BrandId) ? 0 : Convert.ToInt32(form.BrandId);
            Int32 categoryId = string.IsNullOrWhiteSpace(form.CategoryId) ? 0 : Convert.ToInt32(form.CategoryId);
            double priceMin = string.IsNullOrWhiteSpace(form.PriceMinimum) ? 0 : Convert.ToDouble(form.PriceMinimum);
            double priceMax = string.IsNullOrWhiteSpace(form.PriceMaximum) ? 0 : Convert.ToDouble(form.PriceMaximum);

            //get list of categories
            IList<Int32> cateList = new List<Int32>();
            if (categoryId > 0)
            {
                searchCateList.Clear();
                sourceList = cateDao.GetAll();
                CategorySearchList(categoryId);
                cateList = searchCateList;
            }
            

            using (_dbContext = new ItemDbContext())
            {
                
                if (priceMax > 0)
                {
                    result = _dbContext.Inventories.Where(q =>
                    (q.Title.ToUpper().Contains(form.ProductName) || q.Description.ToUpper().Contains(form.ProductName))
                    && (q.brand_id.Equals(brandId)|| brandId == 0)
                    && (cateList.Contains(q.cate_id) || categoryId == 0)
                    && (q.Color.ToLower().Contains(form.Color.ToLower()) || form.Color == "")
                    && (q.Size.ToLower().Contains(form.Size.ToLower()) || form.Size == "")
                    && (q.Price >= priceMin && q.Price <= priceMax)
                    ).ToList();
                }
                else
                {
                    result = _dbContext.Inventories.Where(q =>
                    (q.Title.ToUpper().Contains(form.ProductName) || q.Description.ToUpper().Contains(form.ProductName))
                    && (q.brand_id.Equals(brandId) || brandId == 0)
                    && (cateList.Contains(q.cate_id) || categoryId == 0)
                    && (q.Color.ToLower().Contains(form.Color.ToLower()) || form.Color == "")
                    && (q.Size.ToLower().Contains(form.Size.ToLower()) || form.Size == "")
                    ).ToList();
                }
            }

            return result;
        }

        public int Add(InventoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = new Inventory
                {
                    Id = dto.ItemId,
                    Title = dto.Name,
                    Description = dto.Description,
                    cate_id = dto.CategoryId,
                    type_id = dto.TypeId,
                    Link = dto.SourceLink,
                    Image = dto.Image,
                    ItemCondition = dto.Condition,
                    Availability = dto.Availability,
                    Price = Convert.ToDouble(dto.Price),
                    Price2 = Convert.ToDouble(dto.SalePrice),
                    Eff_Date = Convert.ToDateTime(dto.EffectiveDate),
                    GTIN = dto.GTIN,
                    brand_id = dto.BrandId,
                    MPN = dto.MPN,
                    group_id = dto.GroupId,
                    Gender = dto.Gender,
                    Age_Group = dto.AgeGroup,
                    Color = dto.Color,
                    Size = dto.Size,
                    Shipping = dto.Shipping,
                    Weight = dto.Weight
                };

                _dbContext.Inventories.Add(model);
                nResult = _dbContext.SaveChanges();
            }
            return nResult;
        }

        public int Edit(InventoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Inventories.FirstOrDefault(q => q.Id.Equals(dto.ItemId));
                if (model != null)
                {
                    model.Title = dto.Name;
                    model.Description = dto.Description;
                    model.cate_id = dto.CategoryId;
                    model.type_id = dto.TypeId;
                    model.Link = dto.SourceLink;
                    model.Image = dto.Image;
                    model.ItemCondition = dto.Condition;
                    model.Availability = dto.Availability;
                    model.Price = Convert.ToDouble(dto.Price);
                    model.Price2 = Convert.ToDouble(dto.SalePrice);
                    model.Eff_Date = Convert.ToDateTime(dto.EffectiveDate);
                    model.GTIN = dto.GTIN;
                    model.brand_id = dto.BrandId;
                    model.MPN = dto.MPN;
                    model.group_id = dto.GroupId;
                    model.Gender = dto.Gender;
                    model.Age_Group = dto.AgeGroup;
                    model.Color = dto.Color;
                    model.Size = dto.Size;
                    model.Shipping = dto.Shipping;
                    model.Weight = dto.Weight;

                    nResult = _dbContext.SaveChanges();
                }
            }
            return nResult;
        }

        public int Delete(InventoryDTO dto)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.Inventories.FirstOrDefault(q => q.Id.Equals(dto.ItemId));
                if (model != null)
                {
                    _dbContext.Inventories.Remove(model);
                    nResult = _dbContext.SaveChanges();
                }
            }
            return nResult;
        }

        public Inventory GetByCode(string code)
        {
            Inventory item;
            using (_dbContext = new ItemDbContext())
            {
                item = _dbContext.Inventories.FirstOrDefault(q => q.Id.Equals(code));
            }

            return item;
        }

        public IList<Inventory> SearchItemsByUser(int userId)
        {
            IList<Inventory> list;
            using (_dbContext = new ItemDbContext())
            {
                var items = _dbContext.UserInventory.Where(q => q.User_Id.Equals(userId))
                                .Select(q => q.Item_Id).ToList();
                list = _dbContext.Inventories.Where(q => items.Contains(q.Id)).ToList();
            }

            return list;
        }

        public Int32 CategorySearchList(Int32 cateId)
        {
            searchCateList.Add(cateId);
            var list = sourceList.Where(q => q.ParentId.Equals(cateId)).Select(q => q.Id).ToList();

            foreach (var item in list)
            {
                CategorySearchList(item);
            }

            return 1;
        }

        public IList<Inventory> GetFilterByCategory(int cateId)
        {
            //get list of categories
            if (cateId > 0)
            {
                searchCateList.Clear();
                sourceList = cateDao.GetAll();
                CategorySearchList(cateId);
            }

            var list = new List<Inventory>();
            using (_dbContext = new ItemDbContext())
            {
                list = _dbContext.Inventories.Where(q => searchCateList.Contains(q.cate_id) || cateId == 0).ToList();
            }

            return list;
        }
    }
}