using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.Service
{
    public class UserServiceImpl : UserService
    {
        private UserDAO _dao = new UserDAOImpl();

        public UserListDTO GetAllUsers()
        {
            var listDto = new UserListDTO {DtoList = new List<UserDTO>(), ErrorList = new List<string>()};

            try
            {
                var parentList = _dao.GetAllUsers();
                listDto.DtoList = parentList.Select(q => new UserDTO
                {
                    UserId = q.Id,
                    Email = q.Email,
                    Password = q.PasswordHash
                }).ToList();

                foreach (var user in listDto.DtoList)
                {
                    var dto = GetUser(user);
                    user.Firstname = dto.Firstname;
                    user.Lastname = dto.Lastname;
                    user.Expired = dto.Expired;
                    user.Address = dto.Address;
                }

            }
            catch (Exception e)
            {
                listDto.ErrorList = new List<string>{e.Message};
            }

            return listDto;
        }

        public UserDTO AddUser(UserDTO input)
        {
            var dto = new UserDTO {ErrorList = new List<string>()};
            try
            {
                int nResult = _dao.SaveDetails(input);

                dto.ErrorList.Add(nResult > 0 ? "User added successfully!" : "Add Failed!");
            }
            catch (Exception e)
            {
                dto = new UserDTO{ErrorList = new List<string>{e.Message}};
            }

            return dto;
        }

        public UserDTO EditUser(UserDTO input)
        {
            var dto = new UserDTO { ErrorList = new List<string>() };
            try
            {
                int nResult = _dao.EditDetails(input);

               
                dto.ErrorList.Add(nResult > 0 ? "User edited successfully!" : "Edit Failed!");
            }
            catch (Exception e)
            {
                dto = new UserDTO { ErrorList = new List<string> { e.Message } };
            }

            return dto;
        }

        public UserDTO DeleteUser(UserDTO input)
        {
            var dto = new UserDTO { ErrorList = new List<string>() };
            try
            {
                int nResult = _dao.DeleteDetails(input);
                dto.ErrorList.Add(nResult > 0 ? "User deleted successfully!" : "Delete Failed!");
            }
            catch (Exception e)
            {
                dto = new UserDTO { ErrorList = new List<string> { e.Message } };
            }

            return dto;
        }

        public IList<UserRoleDTO> GetAllRoles()
        {
            IList<UserRoleDTO> listDto;
            try
            {
                var tempList = _dao.GetRoles();
                listDto = tempList.Select(q => new UserRoleDTO
                {
                    Id = q.Id,
                    Name = q.Name,
                    ErrorList = new List<string>()
                }).ToList();
            }
            catch (Exception e)
            {
                listDto = new List<UserRoleDTO>();
                listDto[0].ErrorList = new List<string>{e.Message};
            }

            return listDto;
        }

        public UserDTO GetUser(UserDTO input)
        {
            var dto = new UserDTO {ErrorList = new List<string>()};

            try
            {
               var model = _dao.GetUserDetails(input);

                if (model != null)
                {
                    dto.UserId = model.UserId;
                    dto.Firstname = model.Firstname;
                    dto.Lastname = model.Lastname;
                    dto.Expired = model.expired;
                    dto.Index = model.Index;
                    dto.Email = _dao.GetUserLoginInfo(input).Email;
                    dto.Address = model.Address;
                }
            }
            catch (Exception e)
            {
                dto = new UserDTO { ErrorList = new List<string> { e.Message } };
            }

            return dto;
        }

        public string AddAdmin(UserDTO input)
        {
            string sRet = "Admin added successfully!";
            try
            {
                input.Password = cUtils.Encrypt(input.Password);
                int nVal = _dao.AddAdminUser(input);
                if (nVal <= 0)
                {
                    sRet = "Adding admin user failed!";
                }
            }
            catch (Exception e)
            {
                sRet = e.Message;
            }

            return sRet;
        }

        public UserDTO AdminLogin(AdminLoginForm form)
        {
            UserDTO dto;
            try
            {
                form.Password = string.IsNullOrWhiteSpace(form.Password) ? "" : cUtils.Encrypt(form.Password);
                dto = _dao.LoginAdmin(form) ??
                      new UserDTO { ErrorList = new List<string> { "Username and Password does not match!" } };
            }
            catch (Exception e)
            {
                dto = new UserDTO{ErrorList = new List<string>{e.Message}};
            }

            return dto;
        }

        public AdminDTO SetLoginKey(AdminDTO input)
        {
            AdminDTO dto;
            try
            {
                var model = new AdminUsers
                {
                    Id = Convert.ToInt32(input.Id),
                    Username = input.Username,
                    Password = input.Password,
                    Security = input.Security
                };

                _dao.SetLoginKey(model);

                dto = input;

            }
            catch (Exception e)
            {
                dto = new AdminDTO{ ErrorList = new List<string>{e.Message}};
            }

            return dto;
        }
    }
}