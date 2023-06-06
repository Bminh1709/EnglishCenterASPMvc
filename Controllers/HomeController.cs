using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EnglishCenter.Models;
using EnglishCenter.Filter;

namespace EnglishCenter.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        MinhcenterEntities db = new MinhcenterEntities();
        // GET: Home
        public ActionResult Index()
        {
            //ViewData["listCart"] = db.ShowCart((int?)Session["us_id"]).ToList();
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string us_gmail, string us_pass)
        {
            int AccCount = db.users.Count(m => m.us_gmail == us_gmail && m.us_pass == us_pass);
            if (AccCount == 1)
            {

                Session["user"] = us_gmail;
                ViewBag.loginState = "loginSuccess";
                TempData["userLoginSuccess"] = "You loged in successfully";

                var getUser = db.users.SingleOrDefault(m => m.us_gmail == us_gmail);



                Session["us_id"] = getUser.us_id;
                Session["us_firstname"] = getUser.us_firstname;
                Session["us_lastname"] = getUser.us_lastname;
                Session["us_img"] = getUser.us_img;
                Session["us_phone"] = getUser.us_phone;
                Session["us_gender"] = getUser.us_gender;

                return View();
            }
            else
            {
                TempData["userLoginFail"] = "Please, check your gmail and password again";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(user newUser, HttpPostedFileBase us_img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int AccCount = db.users.Count(m => m.us_gmail == newUser.us_gmail);
                    if (AccCount != 1)
                    {
                        // Lưu file
                        string rootFolder = Server.MapPath("/public/images/img_user/");
                        string pathImage = rootFolder + us_img.FileName;
                        us_img.SaveAs(pathImage);
                        // Lưu thuộc tính
                        newUser.us_img = "/public/images/img_course/" + us_img.FileName;


                        db.users.Add(newUser);
                        db.SaveChanges();
                        TempData["userSignUpSuccess"] = "Sign up successfully, thank you!";
                    }
                    else
                    {
                        TempData["userSignUpFail"] = "Please, your email has been existed!";
                        return View(newUser);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error_msg = ex.Message;
                    return View("Error");
                }
            }
            return View();

            /* DELETE CODE ABOVE AND DELELTE VALIDATE IN FORM TO RUN VALIDATION OF JS  */
            //int AccCount = db.users.Count(m => m.us_gmail == newUser.us_gmail);
            //if (AccCount == 1)
            //{
            //    TempData["userSignUpFail"] = "Please, your email has been existed!";
            //    return View(newUser);
            //}
            //else
            //{
            //    // Lưu file
            //    string rootFolder = Server.MapPath("/public/images/img_user/");
            //    string pathImage = rootFolder + us_img.FileName;
            //    us_img.SaveAs(pathImage);
            //    // Lưu thuộc tính
            //    newUser.us_img = "/public/images/img_course/" + us_img.FileName;


            //    db.users.Add(newUser);
            //    db.SaveChanges();
            //    TempData["userSignUpSuccess"] = "Sign up successfully, thank you!";
            //    return RedirectToAction("Index");
            //}
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Error(int num)
        {
            switch (num)
            {
                case 400:
                    ViewBag.ErrorMs = "400";
                    break;
                case 500:
                    ViewBag.ErrorMs = "500";
                    break;
                case 404:
                    ViewBag.ErrorMs = "404 - Page not found";
                    break;
            }
            // Pass ErrorMessage to the view or perform other actions
            return View();
        }
    }
}