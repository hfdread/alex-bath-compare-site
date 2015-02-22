using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.DTO
{
    public class InventoryDTO
    {
        public Int32 Index { get; set; }
        [Display(Name = "Code")]
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32 CategoryId { get; set; }
        public Int32 TypeId { get; set; }
        [Display(Name ="Merchant Site")]
        public string SourceLink { get; set; }
        public string Image { get; set; }
        public string Condition { get; set; }
        public string Availability { get; set; }
        [Display(Name = "Regular Price")]
        public string Price { get; set; }
        [Display(Name = "Sale Price")]
        public string SalePrice { get; set; }
        [Display(Name = "Effective Date")]
        public string EffectiveDate { get; set; }
        public string GTIN { get; set; }
        public Int32 BrandId { get; set; }
        public string MPN { get; set; }
        [Display(Name = "Group ID")]
        public string GroupId { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Age Group")]
        public string AgeGroup { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Shipping { get; set; }
        public string Weight { get; set; }

        //unbound properties
        [Display(Name = "Category")]
        public string CateName { get; set; }
        public int selCateIndex { get; set; }

        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        public int selBrandIndex { get; set; }

        public IList<string> ErrorList { get; set; }
        public string Prices { get; set; }//contains the regular and sale price and effectivity date
        [Display(Name = "Shipping Information")]
        public string ShippingInfo { get; set; }//contains the shipping and weith
    }

    public class InventoryListDTO
    {
        public IList<InventoryDTO> DtoList { get; set; }
        public IList<string> ErrorList { get; set; }

        public int TotalCount { get; set; }
    }
}