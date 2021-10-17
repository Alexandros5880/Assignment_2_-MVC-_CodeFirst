using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.ViewModels;
using Assignment_2__MVC__CodeFirst.Static;

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class SchoolsController : Controller
    {
        // GET: Schools
        public ActionResult Index()
        {
            return View(Globals.schoolRepo.GetAll());
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = Globals.schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            return View(schoolViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolViewModel schoolView)
        {
            School school = new School();
            school.Name = schoolView.School.Name;
            school.StartDate = schoolView.School.StartDate;

            if (ModelState.IsValid)
            {
                Globals.schoolRepo.Add(school);
                Globals.DbHundler.Save();
                return RedirectToAction("Index");
            }

            return View(schoolView);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = Globals.schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.School = school;
            schoolViewModel.Courses = school.Courses;
            schoolViewModel.Trainers = school.Trainers;
            schoolViewModel.Students = school.Students;
            return View(schoolViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolViewModel schoolView)
        {
            School school = new School();
            school.Name = schoolView.School.Name;
            school.StartDate = schoolView.School.StartDate;
            school.Courses = schoolView.School.Courses;
            school.Assignments = schoolView.School.Assignments;
            school.Trainers = schoolView.School.Trainers;
            school.Students = schoolView.School.Students;

            if (ModelState.IsValid)
            {
                Globals.schoolRepo.Update(school);
                Globals.DbHundler.Save();
                return RedirectToAction("Index");
            }

            return View(schoolView);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = Globals.schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Globals.schoolRepo.Delete(Globals.schoolRepo.Get(id));
            Globals.DbHundler.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Globals.schoolRepo.Dispose();
                //Globals.courseRepo.Dispose();
                //Globals.assignmentRepo.Dispose();
                //Globals.trainerRepo.Dispose();
                //Globals.studentRepo.Dispose();
                //Globals.DbHundler.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
