using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using EnglishCenter.Filter;
using EnglishCenter.Models;
using PagedList;

namespace EnglishCenter.Controllers
{
    public class CoursesController : Controller
    {
        MinhcenterEntities db = new MinhcenterEntities();

        public ActionResult Index(string filter, int? page)
        {
            List<course> lstCourses = new List<course>();
            if (filter != null)
            {
                ViewBag.filter = filter;
                lstCourses = db.courses.Where(c => c.co_name.Contains(filter)).ToList();
            }    
            if (filter == null)
            {
                lstCourses = db.courses.ToList();
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(lstCourses.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Detail(string co_id)
        {
            var getCourse = db.courses.Find(co_id);
            return View(getCourse);
        }
    }
}