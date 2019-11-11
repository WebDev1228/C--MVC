﻿using Mwh.SampleCRUD.BL.Models;
using System.Web.Mvc;

namespace Mwh.Sample.WebApi.Controllers
{

    public class EmployeeController : BaseController
    {
        public JsonResult Add(Employee emp)
        {
            emp.EmployeeID = 0;
            return Json(EmpDB.Update(emp), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var myResult = EmpDB.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Route("/home/EmpSinglePage/")]
        public ActionResult EmpSinglePage()
        {
            return View();
        }

        public JsonResult GetbyID(int id)
        {
            var Employee = EmpDB.Get(id);
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeDelete(int id = 0)
        {
            return PartialView("_EmployeeDelete", EmpDB.Get(id));
        }

        public ActionResult GetEmployeeEdit(int id = 0)
        {
            return PartialView("_EmployeeEdit", EmpDB.Get(id));
        }

        public ActionResult GetEmployeeList()
        {
            return PartialView("_EmployeeList", EmpDB.ListAll());
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(EmpDB.ListAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Employee postEmployee)
        {
            var myResult = EmpDB.Update(postEmployee);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Employee emp)
        {
            return Json(EmpDB.Update(emp), JsonRequestBehavior.AllowGet);
        }
    }
}