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

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class SchoolsController : Controller
    {
        private RepositoryHundler _repoHundler = new RepositoryHundler();
        private SchoolRepo _schoolRepo;
        private CourseRepo _courseRepo;
        private AssignmentRepo _assignmentRepo;
        private TrainerRepo _trainerRepo;
        private StudentRepo _studentRepo;

        public SchoolsController()
        {
            this._schoolRepo = this._repoHundler.GetSchoolRepo();
            this._courseRepo = this._repoHundler.GetCourseRepo();
            this._assignmentRepo = this._repoHundler.GetAssignmentRepo();
            this._trainerRepo = this._repoHundler.GetTrainerRepo();
            this._studentRepo = this._repoHundler.GetStudentRepo();
        }

        // GET: Schools
        public ActionResult Index()
        {
            return View(this._schoolRepo.GetAll());
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = this._schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View(new School());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StartDate")] School school)
        {
            if (ModelState.IsValid)
            {
                this._schoolRepo.Add(school);
                this._repoHundler.Save();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = this._schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StartDate")] School school)
        {
            if (ModelState.IsValid)
            {
                this._schoolRepo.Update(school);
                this._repoHundler.Save();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = this._schoolRepo.Get(id);
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
            this._schoolRepo.Delete(this._schoolRepo.Get(id));
            this._repoHundler.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repoHundler.Dispose();
                this._schoolRepo.Dispose();
                this._courseRepo.Dispose();
                this._assignmentRepo.Dispose();
                this._trainerRepo.Dispose();
                this._studentRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
