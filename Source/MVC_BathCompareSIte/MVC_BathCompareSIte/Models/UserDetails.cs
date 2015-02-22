using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace MVC_BathCompareSIte.Models
{
    public class UserDetails
    {
        [Key]
        public Int32 Index { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public bool expired { get; set; }
    }
}