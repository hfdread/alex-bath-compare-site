using System;
using System.Collections.Generic;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public interface UserDAO
    {
        int SaveDetails(UserDTO input);
        int EditDetails(UserDTO input);
        int DeleteDetails(UserDTO input);

        IList<Aspnetusers> GetAllUsers();
        UserDetails GetUserDetails(UserDTO input);
        IList<Aspnetroles> GetRoles();
        int AddUserRole(UserDTO user, UserRoleDTO role);

        Aspnetusers GetUserLoginInfo(UserDTO input);

        int AddAdminUser(UserDTO input);
        UserDTO LoginAdmin(AdminLoginForm form);
    }
}