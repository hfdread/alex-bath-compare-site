using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;

namespace MVC_BathCompareSIte.Service
{
    public interface UserService
    {
        UserListDTO GetAllUsers();
        UserDTO AddUser(UserDTO input);
        UserDTO EditUser(UserDTO inout);
        UserDTO DeleteUser(UserDTO input);
        IList<UserRoleDTO> GetAllRoles();

        UserDTO GetUser(UserDTO input);
        string AddAdmin(UserDTO input);
        UserDTO AdminLogin(AdminLoginForm input);
    }
}