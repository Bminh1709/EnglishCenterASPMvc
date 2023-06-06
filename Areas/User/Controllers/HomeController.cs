using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnglishCenter.Models;

namespace EnglishCenter.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        MinhcenterEntities db = new MinhcenterEntities();

        // GET: User/Home
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Home/Login");
            }
            else
            {
                List<getCoursesJoined_Result> newListJoined = db.getCoursesJoined((int?)Session["us_id"]).ToList();
               
                return View(newListJoined);
            }
        }

        public ActionResult AddCart(string co_id, int us_id)
        {
            int cartCount = db.user_cart.Count(m => m.co_id == co_id && m.us_id == us_id);

            if (cartCount != 1)
            {
                db.addCart(co_id, us_id);
                db.SaveChanges();
            }
            else
            {
                TempData["CourseInCart"] = "<script>alert('You joined this course, please attend all your classes!');</script>";
            }
            return Redirect("~/courses");

        }

        public ActionResult DeleteCart(string co_id, int us_id)
        {
            db.DeleteCourseInCart(co_id, us_id);
            return RedirectToAction("Invoice");
        }


        public ActionResult Invoice()
        {
            List<getCourseFromCart_Result> newListCourses = db.getCourseFromCart((int?)Session["us_id"]).ToList();
            int totalPrice = 0;
            foreach (var item in newListCourses)
            {
                totalPrice = (int)(totalPrice + item.co_price);
            }
            ViewData["TotalPrice"] = totalPrice.ToString("#,##0");
            Session["TotalPrice"] = totalPrice;
            //ViewData["TotalPrice"] = totalPrice;
            return View(newListCourses);
        }

        public ActionResult Payment()
        {
            int userID = (int)Session["us_id"];
            // Count in orders to get id
            int orderCount = db.orders.Count();
            int newOrderID = orderCount + 1;

            // Insert into order using above id
            db.AddToOrder(newOrderID, (int?)Session["us_id"], (int?)Session["TotalPrice"]);

            // Insert from cart into user_wait using above id
            List<user_cart> ListCartOfUser = db.user_cart.Where(m => m.us_id == userID).ToList();
            foreach (var item in ListCartOfUser)
            {
                user_wait newUserWait = new user_wait();

                newUserWait.co_id = item.co_id;
                newUserWait.us_id = (int?)Session["us_id"];
                newUserWait.or_id = newOrderID;

                db.user_wait.Add(newUserWait);
                db.SaveChanges();
            }
            return Redirect("~/User/Home");
        }    
    }
}