using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnglishCenter.Filter;
using EnglishCenter.Models;

namespace EnglishCenter.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        MinhcenterEntities db = new MinhcenterEntities();

        [CustomAuthentication]
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                List<course> listCourses = new List<course>();
                listCourses = db.courses.ToList();

                return View(listCourses);
            }
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(course newCourse)
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string rootFolder = Server.MapPath("~/public/images/img_course/");
                    string fileName = Path.GetFileName(file.FileName);
                    string pathImage = Path.Combine(rootFolder, fileName);
                    file.SaveAs(pathImage);
                    newCourse.co_img = "/public/images/img_course/" + fileName;
                }

                newCourse = db.courses.Add(newCourse);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
        public ActionResult Update(string idCourse)
        {
            var updateCourse = db.courses.SingleOrDefault(h => h.co_id == idCourse);
            return View(updateCourse);

            //var updateCourse = db.courses.SingleOrDefault(h => h.co_id == idCourse);
            //if (updateCourse != null)
            //{
            //    return Json(new { success = true });
            //}
            //return Json(new { success = false });
            //return View(updateCourse);
        }
        [HttpPost]
        public ActionResult Update(course changedCourse)
        {
            // Tìm ra đối tượng khách hàng
            var updateCourse = db.courses.Find(changedCourse.co_id);
            updateCourse.co_name = changedCourse.co_name;
            updateCourse.co_teacher = changedCourse.co_teacher;
            updateCourse.co_des = changedCourse.co_des;
            updateCourse.co_startday = changedCourse.co_startday;
            updateCourse.co_endday = changedCourse.co_endday;
            updateCourse.co_price = changedCourse.co_price;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE USING AJAX AND NOT RELOAD PAGE
        [HttpPost]
        public ActionResult Delete(string id)
        {
            // Tìm ra đối tượng khách hàng
            var deleteCourse = db.courses.Find(id);
            if (deleteCourse != null)
            {
                // Thực hiện xóa
                db.courses.Remove(deleteCourse);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Orders()
        {
            List<getCoursesUnconfirm_Result> newListCoursesUnconfirm = db.getCoursesUnconfirm().ToList();
            return View(newListCoursesUnconfirm);
        }

        public ActionResult ConfirmCourses(int or_id)
        {
            var confirmOrder = db.orders.SingleOrDefault(x => x.or_id == or_id);
            confirmOrder.or_state = "1";
            db.SaveChanges();
            return RedirectToAction("Orders");
        }    

        public ActionResult UpdateProfile (string lastname, string firstname)
        {
            
            try
            {
                int id = (int)Session["ad_id"];

                var AdminFound = db.admins.SingleOrDefault(x => x.ad_id == id);
                AdminFound.ad_firstname = firstname;
                AdminFound.ad_lastname = lastname;
                db.SaveChanges();

                // Set session again
                Session["ad_firstname"] = firstname;
                Session["ad_lastname"] = lastname;

                return Json( new { success = true });
            }
            catch (Exception)
            {
                return Json( new { success = false });
            }
        }

        public ActionResult UpdateProfilePhoto()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string rootFolder = Server.MapPath("~/public/images/img_admin/");
                    string fileName = Path.GetFileName(file.FileName);
                    string pathImage = Path.Combine(rootFolder, fileName);
                    file.SaveAs(pathImage);

                    string newPhoto = "/public/images/img_admin/" + fileName;

                    int id = (int)Session["ad_id"];
                    var updateAdmin = db.admins.Find(id);
                    updateAdmin.ad_img = newPhoto;
                    db.SaveChanges();

                    Session["ad_img"] = newPhoto;

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "No file selected." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}