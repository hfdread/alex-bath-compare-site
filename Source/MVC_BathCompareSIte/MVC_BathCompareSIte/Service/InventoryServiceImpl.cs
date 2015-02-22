using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Web.UI;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.Models;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.Service
{
    public class InventoryServiceImpl : InventoryService
    {
        private InventoryDao _dao = new InventoryDaoImpl();
        private TypeDao _typeDao = new TypeDAOImpl();
        private BrandDao _brandDao = new BrandDaoImpl();
        private CategoryDao _cateDao = new CategoryDaoImpl();

        public InventoryDao Dao
        {
            get { return this._dao; }
            set { this._dao = value; }
        }

        public InventoryListDTO Search(SearchForm form)
        {
            var resultList = new InventoryListDTO();
            resultList.DtoList = new List<InventoryDTO>();
            resultList.ErrorList = new List<string>();

            try
            {
                form.ProductName = string.IsNullOrWhiteSpace(form.ProductName) ? "" : form.ProductName.ToUpper();
                form.BrandId = string.IsNullOrWhiteSpace(form.BrandId) || form.BrandId.Equals("-1") ? "" : form.BrandId;
                form.CategoryId = string.IsNullOrWhiteSpace(form.CategoryId) || form.CategoryId.Equals("-1") ? "" : form.CategoryId;
                form.PriceMinimum = string.IsNullOrWhiteSpace(form.PriceMinimum) ? "" : form.PriceMinimum;
                form.PriceMaximum = string.IsNullOrWhiteSpace(form.PriceMaximum) ? "" : form.PriceMaximum;
                form.OtherFilter = string.IsNullOrWhiteSpace(form.OtherFilter) ? "" : form.OtherFilter.Trim();
                form.Color = string.IsNullOrWhiteSpace(form.Color) ? "" : form.Color;
                form.Size = string.IsNullOrWhiteSpace(form.Size) ? "" : form.Size;

                var result = this._dao.SearchInventories(form);

                if (!string.IsNullOrWhiteSpace(form.OtherFilter))
                {
                    if (form.OtherFilter.Equals("priceDown"))
                    {
                        result = result.OrderByDescending(q => q.Price)
                            .ThenBy(q => q.Title)
                            .ToList();
                    }
                    else if (form.OtherFilter.Equals("priceUp"))
                    {
                        result = result.OrderBy(q => q.Price)
                            .ThenBy(q => q.Title)
                            .ToList();
                    }
                }

                resultList.DtoList = result.Select(q => new InventoryDTO
                {
                    ItemId = q.Id,
                    Name = q.Title,
                    Description = q.Description,
                    CategoryId = q.cate_id,
                    TypeId = q.type_id,
                    SourceLink = q.Link,
                    Image = q.Image,
                    Condition = q.ItemCondition,
                    Availability = q.Availability,
                    Price = q.Price.Equals(0) ? "0.00" : q.Price.ToString(Common.PRICE_FMT),
                    SalePrice = q.Price2.Equals(0) ? "0.00" : q.Price2.ToString(Common.PRICE_FMT),
                    EffectiveDate = q.Eff_Date.ToShortDateString(),
                    GTIN = q.GTIN,
                    BrandId = q.brand_id,
                    MPN = q.MPN,
                    GroupId = q.group_id,
                    Gender = q.Gender,
                    AgeGroup = q.Age_Group,
                    Color = q.Color,
                    Size = q.Size,
                    Shipping = q.Shipping,
                    Weight = q.Weight,
                    CateName = _cateDao.GetById(q.cate_id).Name,
                    selCateIndex = q.cate_id,
                    BrandName = _brandDao.GetById(q.brand_id).Name,
                    selBrandIndex = q.brand_id,
                    ShippingInfo = string.Format("{0}<br />Weight:{1}", q.Shipping, q.Weight),
                    Prices = string.Format("{0}<br />On Sale:{1}, effective on {2}", 
                                        q.Price.Equals(0) ? "0.00" : q.Price.ToString(Common.PRICE_FMT),
                                        q.Price2.Equals(0) ? "0.00" : q.Price2.ToString(Common.PRICE_FMT),
                                        q.Eff_Date.ToShortDateString())
                }).ToList();
            }
            catch (Exception e)
            {
                resultList = new InventoryListDTO {ErrorList = new List<string> {e.Message}};
            }

            return resultList;
        }

        public InventoryDTO ImportFile(Stream input)
        {
            InventoryDTO result = new InventoryDTO {ErrorList =  new List<string>()};
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Id"),
                    new DataColumn("Title"),
                    new DataColumn("Description"), 
                    new DataColumn("Category"), 
                    new DataColumn("Type"), 
                    new DataColumn("Link"), 
                    new DataColumn("Image"), 
                    new DataColumn("ItemCondition"), 
                    new DataColumn("Availability"), 
                    new DataColumn("Price"), 
                    new DataColumn("Price2"), 
                    new DataColumn("Eff_Date"), 
                    new DataColumn("GTIN"), 
                    new DataColumn("Brand"), 
                    new DataColumn("MPN"), 
                    new DataColumn("group_id"), 
                    new DataColumn("Gender"), 
                    new DataColumn("Age_Group"), 
                    new DataColumn("Color"), 
                    new DataColumn("Size"), 
                    new DataColumn("Shipping"), 
                    new DataColumn("Weight")
                });

                bool bHeaders = false;
                int nRow = 1;
                using (var sr = new StreamReader(input))
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = sr.ReadLine().Split('\t');
                        string errorVal = RowValidation(line,nRow);
                        if (string.IsNullOrWhiteSpace(errorVal))
                        {
                            dt.Rows.Add(line);
                        }
                        else
                        {
                            result.ErrorList.Add(errorVal);
                            break;
                        }

                        nRow++;
                    }
                }

                //Check of errors
                if (result.ErrorList.Count <= 0)
                {
                    //Add data to actual table
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row["Id"].ToString().ToLower().Equals("id"))//skip headers
                        {
                            var dto = new InventoryDTO();
                            dto.ItemId = row["Id"].ToString();
                            dto.Name = row["Title"].ToString();
                            dto.Description = row["Description"].ToString();
                            dto.SourceLink = row["Link"].ToString();
                            dto.Image = row["Image"].ToString();
                            dto.Condition = row["ItemCondition"].ToString();
                            dto.Availability = row["Availability"].ToString();
                            dto.Price = cUtils.Format_Price(row["Price"].ToString());
                            dto.SalePrice = cUtils.Format_Price(row["Price2"].ToString());
                            dto.EffectiveDate = cUtils.Format_Date(row["Eff_Date"].ToString());
                            dto.GTIN = cUtils.Format_GTIN(row["GTIN"].ToString());
                            dto.MPN = row["MPN"].ToString();
                            dto.GroupId = row["group_id"].ToString();
                            dto.Gender = row["Gender"].ToString();
                            dto.AgeGroup = row["Age_Group"].ToString();
                            dto.Color = row["Color"].ToString();
                            dto.Size = row["Size"].ToString();
                            dto.Shipping = row["Shipping"].ToString();
                            dto.Weight = row["Weight"].ToString();

                            //get the category
                            string[] cateList = row["Category"].ToString().Split('>');
                            int parentId = 0;
                            for (int i = 0; i < cateList.Length; i++)
                            {
                                if (_cateDao.GetByName(cateList[i].Trim()) == null) //register
                                {
                                    CategoryDTO cateDto = new CategoryDTO();
                                    cateDto.Name = cateList[i].Trim();
                                    cateDto.ParentCategoryId = parentId.ToString();
                                    _cateDao.Add(cateDto);
                                }

                                parentId = _cateDao.GetByName(cateList[i].Trim()).Id;
                            }
                            //assign category
                            dto.CategoryId = parentId;

                            //get the type
                            string[] typeList = row["Type"].ToString().Split('>');
                            parentId = 0;
                            for (int y = 0; y < typeList.Length; y++)
                            {
                                if (_typeDao.GetByName(typeList[y].Trim()) == null) //register
                                {
                                    TypeDTO typeDto = new TypeDTO();
                                    typeDto.Name = typeList[y].Trim();
                                    typeDto.Parent = parentId;
                                    _typeDao.Add(typeDto);
                                }

                                parentId = _typeDao.GetByName(typeList[y].Trim()).Id;
                            }
                            //assign type
                            dto.TypeId = parentId;

                            //get the brand
                            var brand = _brandDao.GetByName(row["Brand"].ToString());
                            if (brand == null)
                            {
                                BrandDTO brandDto = new BrandDTO();
                                brandDto.Name = row["Brand"].ToString();
                                _brandDao.Add(brandDto);

                                brand = _brandDao.GetByName(row["Brand"].ToString());
                            }
                            //assign brand
                            dto.BrandId = brand.Id;

                            _dao.Add(dto);
                        }

                    }//end of result.ErrorList checking
                }

            }
            catch (Exception e)
            {
                result = new InventoryDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public InventoryDTO Add(InventoryDTO dto)
        {
            var result = new InventoryDTO {ErrorList = new List<string>()};
            try
            {
                dto.Description = string.IsNullOrWhiteSpace(dto.Description) ? "" : dto.Description.Trim();
                dto.SourceLink = string.IsNullOrWhiteSpace(dto.SourceLink) ? "" : dto.SourceLink.Trim();
                dto.Condition = string.IsNullOrWhiteSpace(dto.Condition) ? "" : dto.Condition.Trim();
                dto.Availability = string.IsNullOrWhiteSpace(dto.Availability) ? "" : dto.Availability.Trim();
                dto.EffectiveDate = string.IsNullOrWhiteSpace(dto.EffectiveDate) ? DateTime.Now.ToShortDateString() : dto.EffectiveDate.Trim();
                dto.GTIN = string.IsNullOrWhiteSpace(dto.GTIN) ? "" : dto.GTIN.Trim();
                dto.MPN = string.IsNullOrWhiteSpace(dto.MPN) ? "" : dto.MPN.Trim();
                dto.GroupId = string.IsNullOrWhiteSpace(dto.GroupId) ? "" : dto.GroupId.Trim();
                dto.Gender = string.IsNullOrWhiteSpace(dto.Gender) ? "" : dto.Gender.Trim();
                dto.AgeGroup = string.IsNullOrWhiteSpace(dto.AgeGroup) ? "" : dto.AgeGroup.Trim();
                dto.Color = string.IsNullOrWhiteSpace(dto.Color) ? "" : dto.Color;
                dto.Size = string.IsNullOrWhiteSpace(dto.Size) ? "" : dto.Size;
                dto.Shipping = string.IsNullOrWhiteSpace(dto.Shipping) ? "" : dto.Shipping;
                dto.Weight = string.IsNullOrWhiteSpace(dto.Weight) ? "" : dto.Weight;

                dto.Image = string.IsNullOrWhiteSpace(dto.Image) ? "noimage.jpg" : dto.Image;
                dto.Price = string.IsNullOrWhiteSpace(dto.Price) ? "0" : dto.Price;
                dto.SalePrice = string.IsNullOrWhiteSpace(dto.SalePrice) ? "0" : dto.SalePrice;

                int nResult = _dao.Add(dto);
                if (nResult == 0)
                {
                    result.ErrorList.Add("Add Failed!");
                }
                else
                {
                    result = GetByCode(dto.ItemId);
                }
            }
            catch (Exception e)
            {
                result = new InventoryDTO{ErrorList = new List<string>{ e.Message }};
            }

            return result;
        }

        public InventoryDTO Edit(InventoryDTO dto)
        {
            var result = new InventoryDTO {ErrorList = new List<string>()};
            try
            {
                dto.BrandId = _brandDao.GetById(dto.selBrandIndex) == null ? 0 : _brandDao.GetById(dto.selBrandIndex).Id;
                dto.CategoryId = _cateDao.GetById(dto.selCateIndex) == null ? 0 : _cateDao.GetById(dto.selCateIndex).Id;
                dto.Price = string.IsNullOrWhiteSpace(dto.Price) ? "0" : dto.Price.Trim();
                dto.SalePrice = string.IsNullOrWhiteSpace(dto.SalePrice) ? "0" : dto.SalePrice.Trim();

                int nResult = _dao.Edit(dto);
                if (nResult == 0)
                {
                    result.ErrorList.Add("Edit Failed!");
                }
                else
                {
                    result = GetByCode(dto.ItemId);
                }
            }
            catch (Exception e)
            {
                result = new InventoryDTO { ErrorList = new List<string> { e.Message } };
            }

            return result;
        }

        public InventoryDTO Delete(InventoryDTO dto)
        {
            var result = new InventoryDTO { ErrorList = new List<string>() };
            try
            {
                dto.BrandId = _brandDao.GetById(dto.selBrandIndex) == null ? 0 : _brandDao.GetById(dto.selBrandIndex).Id;
                dto.CategoryId = _cateDao.GetById(dto.selCateIndex) == null ? 0 : _cateDao.GetById(dto.selCateIndex).Id;
                dto.Price = string.IsNullOrWhiteSpace(dto.Price) ? "0" : dto.Price.Trim();
                dto.SalePrice = string.IsNullOrWhiteSpace(dto.SalePrice) ? "0" : dto.SalePrice.Trim();

                int nResult = _dao.Delete(dto);
                if (nResult == 0)
                {
                    result.ErrorList.Add("Delete Failed!");
                }
            }
            catch (Exception e)
            {
                result = new InventoryDTO { ErrorList = new List<string> { e.Message } };
            }

            return result;
        }

        public InventoryDTO GetByCode(string code)
        {
            var result = new InventoryDTO { ErrorList = new List<string>() };
            try
            {
                var model = _dao.GetByCode(code);

                if (model != null)
                {
                    result.Index = model.Index;
                    result.AgeGroup = model.Age_Group;
                    result.Availability = model.Availability;
                    result.Color = model.Color;
                    result.Condition = model.ItemCondition;
                    result.EffectiveDate = model.Eff_Date.ToShortDateString();
                    result.GTIN = model.GTIN;
                    result.Gender = model.Gender;
                    result.GroupId = model.group_id;
                    result.Image = model.Image;
                    result.ItemId = model.Id;
                    result.MPN = model.MPN;
                    result.Name = model.Title;
                    result.Price = model.Price.Equals(0) ? "0.00" : model.Price.ToString(Common.PRICE_FMT);
                    result.SalePrice = model.Price.Equals(0) ? "0.00" : model.Price2.ToString(Common.PRICE_FMT);
                    result.Shipping = model.Shipping;
                    result.Size = model.Size;
                    result.SourceLink = model.Link;
                    result.TypeId = model.type_id;
                    result.Weight = model.Weight;
                    result.Description = model.Description;
                    result.CateName = _cateDao.GetById(model.cate_id).Name;
                    result.selCateIndex = model.cate_id;
                    result.BrandName = _brandDao.GetById(model.brand_id).Name;
                    result.selBrandIndex = model.brand_id;
                    result.ShippingInfo = string.Format("{0}<br />Weight:{1}", model.Shipping, model.Weight);
                    result.Prices = string.Format("{0}<br />On Sale:{1}, effective on {2}", result.Price,
                                                    result.SalePrice, result.EffectiveDate);
                }
            }
            catch (Exception e)
            {
                result = new InventoryDTO { ErrorList = new List<string> { e.Message } };
            }

            return result;
        }

        public string RowValidation(string[] rowVal, int nRow)
        {
            //validation message
            string strRet = "";

            if (rowVal.Length > 22 || rowVal.Length < 22)
            {
                strRet = string.Format("Row #{0} is invalid", nRow);
            }

            return strRet;
        }

        public InventoryListDTO SearchItemsByUser(Int32 userId)
        {
            var listDto = new InventoryListDTO
            {
                ErrorList = new List<string>(),
                DtoList = new List<InventoryDTO>(),
                TotalCount = 0
            };

            try
            {
                listDto.DtoList = _dao.SearchItemsByUser(userId).Select(q => new InventoryDTO
                                    {
                                        ItemId = q.Id,
                                        Name = q.Title,
                                        Description = q.Description,
                                        CategoryId = q.cate_id,
                                        TypeId = q.type_id,
                                        SourceLink = q.Link,
                                        Image = q.Image,
                                        Condition = q.ItemCondition,
                                        Availability = q.Availability,
                                        Price = q.Price.Equals(0) ? "0.00" : q.Price.ToString(Common.PRICE_FMT),
                                        SalePrice = q.Price2.Equals(0) ? "0.00" : q.Price2.ToString(Common.PRICE_FMT),
                                        EffectiveDate = q.Eff_Date.ToShortDateString(),
                                        GTIN = q.GTIN,
                                        BrandId = q.brand_id,
                                        MPN = q.MPN,
                                        GroupId = q.group_id,
                                        Gender = q.Gender,
                                        AgeGroup = q.Age_Group,
                                        Color = q.Color,
                                        Size = q.Size,
                                        Shipping = q.Shipping,
                                        Weight = q.Weight,
                                        CateName = _cateDao.GetById(q.cate_id).Name,
                                        selCateIndex = q.cate_id,
                                        BrandName = _brandDao.GetById(q.brand_id).Name,
                                        selBrandIndex = q.brand_id,
                                        ShippingInfo = string.Format("{0}<br />Weight:{1}", q.Shipping, q.Weight),
                                        Prices = string.Format("{0}<br />On Sale:{1}, effective on {2}",
                                                            q.Price.Equals(0) ? "0.00" : q.Price.ToString(Common.PRICE_FMT),
                                                            q.Price2.Equals(0) ? "0.00" : q.Price2.ToString(Common.PRICE_FMT),
                                                            q.Eff_Date.ToShortDateString())
                                    }).ToList();

                listDto.TotalCount = listDto.DtoList.Count;
            }
            catch (Exception e)
            {
                listDto = new InventoryListDTO();
                listDto.ErrorList = new List<string>{e.Message};
            }

            return listDto;
        }

        public IList<ColorTreeDto> GetColorsByCategory(int Id)
        {
            var list = new List<ColorTreeDto>();
            try
            {
                var modelList = _dao.GetFilterByCategory(Id);
                foreach (var item in modelList)
                {
                    if (!string.IsNullOrWhiteSpace(item.Color))
                    {
                        if (item.Color.Contains("/"))
                        {
                            var colorSplit = item.Color.Split('/');
                            foreach (var strItem in colorSplit)
                            {
                                if (!list.Any(q => q.text.Equals(strItem)))
                                {
                                    list.Add(new ColorTreeDto
                                    {
                                        id = strItem,
                                        text = strItem
                                    });
                                }
                            }
                        }
                        else
                        {
                            if (!list.Any(q => q.text.Equals(item.Color)))
                            {
                                list.Add(new ColorTreeDto
                                {
                                    id = item.Color,
                                    text = item.Color
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public IList<SizeTreeDto> GetSizesByCategory(int Id)
        {
            var list = new List<SizeTreeDto>();
            try
            {
                var modelList = _dao.GetFilterByCategory(Id);
                foreach (var item in modelList)
                {
                    if (!list.Any(q => q.text.Equals(item.Size)) 
                            && !string.IsNullOrWhiteSpace(item.Size))
                    {
                        list.Add(new SizeTreeDto
                        {
                            id = item.Size,
                            text = item.Size
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }
    }
}