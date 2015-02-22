using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_BathCompareSIte.Models
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32 ParentId { get; set; }
    }
}