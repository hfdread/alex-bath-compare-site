using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.DAO
{
    public class UserDAOImpl : UserDAO
    {
        private ItemDbContext _dbContext;

        public int SaveDetails(UserDTO input)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = new UserDetails
                {
                    Address = input.Address,
                    Firstname = input.Firstname,
                    Lastname = input.Lastname,
                    UserId = input.UserId,
                    expired = input.Expired
                };

                _dbContext.UserDetails.Add(model);
                nResult = _dbContext.SaveChanges();
            }

            return nResult;
        }

        public int EditDetails(UserDTO input)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.UserDetails.FirstOrDefault(q => q.UserId.Equals(input.UserId));

                if (model != null)
                {
                    model.Address = string.IsNullOrWhiteSpace(input.Address) ? "" : input.Address;
                    model.Firstname = string.IsNullOrWhiteSpace(input.Firstname) ? "" : input.Firstname;
                    model.Lastname = string.IsNullOrWhiteSpace(input.Lastname) ? "" : input.Lastname;
                    model.expired = input.Expired;

                    nResult = _dbContext.SaveChanges();
                }
            }

            return nResult;
        }

        public int DeleteDetails(UserDTO input)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.UserDetails.FirstOrDefault(q => q.UserId.Equals(input.UserId));

                if (model != null)
                {
                    var appUser = _dbContext.AppUsers.FirstOrDefault(q => q.Id.Equals(input.UserId));
                    _dbContext.UserDetails.Remove(model);
                    _dbContext.AppUsers.Remove(appUser);

                    nResult = _dbContext.SaveChanges();
                }
            }

            return nResult;
        }

        public IList<Aspnetusers> GetAllUsers()
        {
            List<Aspnetusers> list;
            using (_dbContext = new ItemDbContext())
            {
               list = _dbContext.AppUsers.ToList();
            }

            return list;
        }

        public UserDetails GetUserDetails(UserDTO input)
        {
            UserDetails model;
            using (_dbContext = new ItemDbContext())
            {
                model = _dbContext.UserDetails.FirstOrDefault(q => q.UserId.Equals(input.UserId));
            }

            return model;
        }

        public IList<Aspnetroles> GetRoles()
        {
            List<Aspnetroles> list;
            using (_dbContext = new ItemDbContext())
            {
                list = _dbContext.AppRoles.ToList();
            }
            return list;
        }

        public int AddUserRole(UserDTO user, UserRoleDTO role)
        {
            int nResult = 0;
            using (_dbContext = new ItemDbContext())
            {
                var model = _dbContext.AppUserRoleRelation.FirstOrDefault(q => q.UserId.Equals(user.UserId));
                if (model != null)
                {
                    model.RoleId = role.Id;
                }
                else
                {
                    model = new Aspnetuserroles {RoleId = role.Id, UserId = user.UserId};
                    _dbContext.AppUserRoleRelation.Add(model);
                }

                nResult = _dbContext.SaveChanges();
            }

            return nResult;
        }

        public Aspnetusers GetUserLoginInfo(UserDTO input)
        {
            Aspnetusers model;
            using (_dbContext = new ItemDbContext())
            {
                model = _dbContext.AppUsers.FirstOrDefault(q => q.Id.Equals(input.UserId));
            }

            return model;
        }

        public int AddAdminUser(UserDTO input)
        {
            int nRet;
            using (_dbContext = new ItemDbContext())
            {
                var model = new AdminUsers {Username = input.Email, Password = input.Password, Security = ""};

                _dbContext.AdminUsers.Add(model);
                nRet = _dbContext.SaveChanges();
            }

            return nRet;
        }

        public UserDTO LoginAdmin(AdminLoginForm form)
        {
            UserDTO dto = null;
            using (_dbContext = new ItemDbContext())
            {
                var model =_dbContext.AdminUsers.FirstOrDefault(
                                    q => q.Username.Equals(form.Username) && q.Password.Equals(form.Password));
                if (model != null)
                {
                    dto = new UserDTO {Email = model.Username};
                }

            }
            return dto;
        }
    }
}