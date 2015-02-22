using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Service;

namespace MVC_BathCompareSIte.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We are here to answer any questions you may have about your Badkamers experience. Reach out to us" +
                              " and we'll respond as soon as we can. <br/><br/>" +
                              "Even if there is something you have always wanted to experience and can't find it on Badkamers. Let us know" +
                              " and we promise we'll do our best to find it for you and give it to you.";

            return View();
        }

        public JsonResult GetCategories()
        {
            CategoryService service = new CategoryServiceImpl();
            var result = service.GetAll();

            return Json(result.DtoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBrands()
        {
            BrandService service = new BrandServiceImpl();
            var result = service.GetAll();

            return Json(result.DtoList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProducts(SearchForm form)
        {
            InventoryService service = new InventoryServiceImpl();
            var list = service.Search(form);

            return Json(list.DtoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("_GetCategories")]
        public JsonResult GetCategoryTree(int? Id)
        {
            CategoryService service = new CategoryServiceImpl();

            var list = service.GetAll();
            list.DtoList.Insert(0, new CategoryDTO
            {
                Id = -1,
                Name = "All",
                ParentCategoryId = "0"
            });
            var dtoList = list.DtoList.Where(q => q.ParentCategoryId.Equals("0")).Select(q => new
            {
                id = q.Id,
                text = q.Name,
                hasChildren = service.HasChild(q.Id)
            }).ToList();

            if (Id != null)
            {
                dtoList = list.DtoList.Where(q => q.ParentCategoryId.Equals(Id.ToString())).Select(q => new
                {
                    id = q.Id,
                    text = q.Name,
                    hasChildren = service.HasChild(q.Id)
                }).ToList();
            }
            return Json(dtoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("_GetBrandList")]
        public JsonResult GetBrandTree(int? Id)
        {
            BrandService service = new BrandServiceImpl();
            var list = service.GetAll();
            list.DtoList.Insert(0, new BrandDTO
            {
                Id = -1,
                Name = "Any"
            });

            var dtoList = list.DtoList.Select(q => new
            {
                id = q.Id,
                text = q.Name,
                hasChildren = false
            }).ToList();

            if (Id != null)
            {
                dtoList = list.DtoList.Where(q => q.Id.Equals(Id)).Select(q => new
                {
                    id = q.Id,
                    text = q.Name,
                    hasChildren = false
                }).ToList();
            }

            return Json(dtoList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("_GetColors")]
        public JsonResult GetColorOfCategory(int? Id)
        {
            InventoryService service = new InventoryServiceImpl();
            var cateId = (int) (Id== null || Id == -1 ? 0 : Id);
            var list = service.GetColorsByCategory(cateId);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [ActionName("_GetSizes")]
        public JsonResult GetSizeOfCategory(int? Id)
        {
            InventoryService service = new InventoryServiceImpl();
            var cateId = (int)(Id == null || Id == -1 ? 0 : Id);
            var list = service.GetSizesByCategory(cateId);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemByCode(string ItemCode)
        {
            InventoryService service = new InventoryServiceImpl();
            var item = service.GetByCode(ItemCode);

            item.Price = DateTime.Today.CompareTo(Convert.ToDateTime(item.EffectiveDate)) >= 0
                                                    ? item.SalePrice
                                                    : item.Price;
            item.Color = string.IsNullOrWhiteSpace(item.Color) ? "N/A" : item.Color;
            item.Shipping = string.IsNullOrWhiteSpace(item.Shipping) ? "N/A" : item.Shipping;
            item.Size = string.IsNullOrWhiteSpace(item.Size) ? "N/A" : item.Size;

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAdmin()
        {
            ViewBag.Title = "Add Admin Page";
            return View();
        }

        public JsonResult AddAdminUser(UserDTO input)
        {
            UserService service = new UserServiceImpl();
            string result = service.AddAdmin(input);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }//end of class
}