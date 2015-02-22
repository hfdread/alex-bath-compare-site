using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_BathCompareSIte.DTO
{
    public class UserDTO
    {
        public Int32 Index { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public bool Expired { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IList<string> ErrorList { get; set; }
    }

    public class UserListDTO
    {
        public IList<UserDTO> DtoList { get; set; }

        public IList<string> ErrorList { get; set; }
    }

    public class UserRoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> ErrorList { get; set; }
    }
}