using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace MVC_BathCompareSIte.Models
{
    public class Inventory
    {
        [Key]
        public Int32 Index { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public Int32 cate_id { get; set; }
        public Int32 type_id { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string ItemCondition { get; set; }
        public string Availability { get; set; }
        public double Price { get; set; }
        public double Price2 { get; set; }
        public DateTime Eff_Date { get; set; }
        public string GTIN { get; set; }
        public Int32 brand_id { get; set; }
        public string MPN { get; set; }
        public string group_id { get; set; }
        public string Gender { get; set; }
        public string Age_Group { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Shipping { get; set; }
        public string Weight { get; set; }
    }
}