using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.Models
{
    public class ProdType
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32? ParentId { get; set; }
    }
}