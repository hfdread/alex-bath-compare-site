using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.UI;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Service;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.Controllers
{
    public class CPanelController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginForm input)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", input);
            }

            //TODO: delete comment after user service and dao implementation
            //UserService service = new UserServiceImpl();
            //UserDTO userDto = service.LoginUser(input);
            //if (userDto != null)
            //{
            //    //improve this part, with a salt randomizer
            //   return RedirectToAction("UserDashboard", new {id = cUtils.Encrypt(userDto.Id.ToString())});
            //}
            //-TODO

             
            return View("Index", input);   
        }

        public ActionResult UserDashboard(string id)
        {
            //TODO: delete comment after user service and dao implementation
            //UserService service = new UserServiceImpl();
            //var user = new UserDTO();
            //string strId = cUtils.Decrypt(id);
            //user = service.GetById(Convert.ToInt32(strId));

            //if (user == null)
            //{
            //    return RedirectToAction("SignIn");
            //}
            //-TODO

            //TODO: delete comment after user service and dao implementation
            //return View(user);
            return View();
        }

        [HttpPost]
        public JsonResult GetItemList(UserCPanelForm form)
        {
            string strId = cUtils.Decrypt(form.Id);
            InventoryService service = new InventoryServiceImpl();

            var list = service.SearchItemsByUser(Convert.ToInt32(strId));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}