using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.Models
{
    public class UserInventories
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 User_Id { get; set; }
        public string Item_Id { get; set; }
    }
}