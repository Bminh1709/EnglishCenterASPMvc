using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EnglishCenter.Models;

namespace EnglishCenter.Areas.Admin.Controllers
{
    public class AccessController : Controller
    {
        // GET: Admin/Access
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string ad_gmail, string ad_pass)
        {
            MinhcenterEntities db = new MinhcenterEntities();
            int AccCount = db.admins.Count(m => m.ad_gmail == ad_gmail && m.ad_pass == ad_pass);

            if (AccCount == 1)
            {
                Session["admin"] = ad_gmail;
                TempData["adminLoginSuccess"] = "You loged in successfully";

                var getAdmin = db.admins.SingleOrDefault(m => m.ad_gmail == ad_gmail);

                FormsAuthentication.SetAuthCookie(getAdmin.ad_gmail, false);

                Session["ad_img"] = getAdmin.ad_img;
                Session["ad_id"] = getAdmin.ad_id;
                Session["ad_firstname"] = getAdmin.ad_firstname;
                Session["ad_lastname"] = getAdmin.ad_lastname;

                return View();
            }    
            else
            {
                TempData["adminLoginFail"] = "Please, check your gmail and password again";
                return View();
            }    
        }

        public ActionResult LogOut()
        {
            Session.Remove("admin");
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }


    }
}