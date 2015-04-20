using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Service;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Site Administrator";
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = "Administrator [Dashboard]";
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Title = "Administrator [User Management]";
            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.Title = "Administrator [Category Management]";
            return View();
        }

        public ActionResult Brands()
        {
            ViewBag.Title = "Administrator [Brands Management]";
            return View();
        }

        public ActionResult Items()
        {
            ViewBag.Title = "Administrator [Item Management]";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(AdminLoginForm form)
        {
            ViewBag.Error = "";
            ViewBag.Title = "Site Administrator";
            if (!ModelState.IsValid)
            {
                return View("Index", form);
            }

            UserService service =new UserServiceImpl();
            UserDTO dto = service.AdminLogin(form);
            if (dto != null && dto.ErrorList == null)
            {
                //Create key for session
                var randKey = cUtils.Encrypt(string.Format("{0}{1}", dto.Email, Session.LCID));

                Session.Add("userkey",randKey);
                return RedirectToAction("Dashboard");
            }
            
            if (dto != null && dto.ErrorList != null)
            {
                ViewBag.Error = dto.ErrorList[0];
                return View("Index", form);
            }

            ViewBag.Error = "Username and password does not match!";
            return View("Index", form);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return View("Index");
        }

        public ActionResult ItemUpload() 
        {
            ViewBag.Title = "Item Bulk Uploader";
            ViewBag.Notes = "Acceptable files are *.txt or *.csv and <strong>tab delimited</strong> for column values.";

            return View("ItemBulkUpload");
        }

        public ActionResult SearchCategories([DataSourceRequest] DataSourceRequest request)
        {
            CategoryService service = new CategoryServiceImpl();
            var list = service.GetAll();

            return Json(list.DtoList.ToDataSourceResult(request));
        }

        public ActionResult SearchProducts([DataSourceRequest] DataSourceRequest request)
        {
            InventoryService service = new InventoryServiceImpl();
            SearchForm form = new SearchForm();
            form.ProductName = "";

            var list = service.Search(form);

            return Json(list.DtoList.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCategory([DataSourceRequest] DataSourceRequest request, CategoryDTO category)
        {
            CategoryService service = new CategoryServiceImpl();
            if (category != null && ModelState.IsValid)
            {
                category = service.Add(category);
            }

            return Json(new[] {category}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCategory([DataSourceRequest] DataSourceRequest request, CategoryDTO category)
        {
            CategoryService service = new CategoryServiceImpl();
            if (category != null && ModelState.IsValid)
            {
                category = service.Edit(category);
                //category = service.GetByID(category.Id);
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteCategory([DataSourceRequest] DataSourceRequest request, CategoryDTO category)
        {
            CategoryService service = new CategoryServiceImpl();
            if (category != null && ModelState.IsValid)
            {
                category = service.Delete(category);
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult SearchBrands([DataSourceRequest] DataSourceRequest request)
        {
            BrandService service = new BrandServiceImpl();
            var list = service.GetAll();

            return Json(list.DtoList.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddBrand([DataSourceRequest] DataSourceRequest request, BrandDTO brand)
        {
            BrandService service = new BrandServiceImpl();
            if (brand != null && ModelState.IsValid)
            {
                brand = service.Add(brand);
            }

            return Json(new[] {brand}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBrand([DataSourceRequest] DataSourceRequest request, BrandDTO brand)
        {
            BrandService service = new BrandServiceImpl();
            if (brand != null && ModelState.IsValid)
            {
                brand = service.Edit(brand);
            }

            return Json(new[] {brand}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteBrand([DataSourceRequest] DataSourceRequest request, BrandDTO brand)
        {
            BrandService service = new BrandServiceImpl();
            if (brand != null && ModelState.IsValid)
            {
                brand = service.Delete(brand);
            }

            return Json(new[] {brand}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ImportFile(UploadFileForm form)
        {
            ViewBag.Title = "Item Bulk Uploader";
            ViewBag.Notes = "Acceptable files are *.txt or *.csv and <strong>tab delimited</strong> for column values.";
            ViewBag.SuccessMessage = "";

            if (Request != null && ModelState.IsValid)
            {
                //HttpPostedFileBase file = Request.Files["uploadedFile"];

                if (form.uploadedFile != null && !string.IsNullOrEmpty(form.uploadedFile.FileName) && form.uploadedFile.ContentLength > 0)
                {
                    InventoryService service = new InventoryServiceImpl();
                    var result = service.ImportFile(form.uploadedFile.InputStream);

                    if (result.ErrorList.Count > 0)
                    {
                        foreach (string str in result.ErrorList)
                        {
                            ModelState.AddModelError("uploadedFile",str);
                        }
                    }
                    else
                    {
                        ViewBag.SuccessMessage = "File Upload has been successfull!";
                    }
                }
                else if (form.uploadedFile != null && form.uploadedFile.ContentLength == 0)
                {
                    ModelState.AddModelError("uploadedFile","No rows to upload");
                }
            }

            return View("ItemBulkUpload", form);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateInventory([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Prices, ShippingInfo")] InventoryDTO dto)
        {
            InventoryService service = new InventoryServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                dto = service.Edit(dto);
                //dto = service.GetByCode(dto.ItemId);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteInventory([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Prices, ShippingInfo")] InventoryDTO dto)
        {
            InventoryService service = new InventoryServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                dto = service.Delete(dto);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddInventory([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Prices, ShippingInfo")] InventoryDTO dto)
        {
            InventoryService service = new InventoryServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                dto = service.Add(dto);
                //dto = service.GetByCode(dto.ItemId);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }

        //User actions
        public ActionResult SearchUsers([DataSourceRequest] DataSourceRequest request)
        {
            UserService service = new UserServiceImpl();
            var list = service.GetAllUsers();

            return Json(list.DtoList.ToDataSourceResult(request));
        }

        public JsonResult GetUserType()
        {
            UserService service = new UserServiceImpl();
            var list = service.GetAllRoles();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateUser([DataSourceRequest] DataSourceRequest request, UserDTO dto)
        {
            UserService service = new UserServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                service.EditUser(dto);
                dto = service.GetUser(dto);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddUser([DataSourceRequest] DataSourceRequest request, UserDTO dto)
        {
            UserService service = new UserServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                dto = service.AddUser(dto);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteUser([DataSourceRequest] DataSourceRequest request, UserDTO dto)
        {
            UserService service = new UserServiceImpl();
            if (dto != null && ModelState.IsValid)
            {
                service.DeleteUser(dto);
            }

            return Json(new[] {dto}.ToDataSourceResult(request, ModelState));
        }
    }
}