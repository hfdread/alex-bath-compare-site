using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_BathCompareSIte.Models
{
    public class Ratings
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public Int32 ItemCode { get; set; }

        [Required]
        public Int32 Rating { get; set; }

        [ForeignKey("ItemCode")]
        public virtual Inventory Item { get; set; }
    }
}