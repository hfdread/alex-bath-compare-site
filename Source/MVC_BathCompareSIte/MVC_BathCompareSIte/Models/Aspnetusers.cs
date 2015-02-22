using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.Models
{
    public class Aspnetusers
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public class Aspnetroles
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Aspnetuserroles
    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

    public class AdminUsers
    {
        [Key]
        public Int32 Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Security { get; set; }
    }
}