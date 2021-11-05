using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.Static;
using Assignment_2__MVC__CodeFirst.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class SchoolsController : Controller
    {
        private SchoolRepo _schoolRepo;
        public SchoolsController(IRepository<School> repo)
        {
            this._schoolRepo = (SchoolRepo)repo;
        }
        // GET: Schools
        public ActionResult Index()
        {
            return View(this._schoolRepo.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Search)
        {
            if (Search != null && Search.Length > 0)
                return View(this._schoolRepo.GetAllByName(Search));
            else
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
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            return View(schoolViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolViewModel schoolView)
        {
            School school = new School();
            school.ID = schoolView.ID;
            school.Name = schoolView.Name;
            school.StartDate = schoolView.StartDate;
            school.Courses = schoolView.Courses;
            school.Assignments = schoolView.Assignments;
            school.Trainers = schoolView.Trainers;
            school.Students = schoolView.Students;
            if (ModelState.IsValid)
            {
                this._schoolRepo.Add(school);
                Repos.DbHundler.Save();
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
            School school = this._schoolRepo.Get(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.ID = school.ID;
            schoolViewModel.Name = school.Name;
            schoolViewModel.StartDate = school.StartDate;
            return View(schoolViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolViewModel schoolView)
        {
            School school = new School();
            school.ID = schoolView.ID;
            school.Name = schoolView.Name;
            school.StartDate = schoolView.StartDate;
            school.Courses = schoolView.Courses;
            school.Assignments = schoolView.Assignments;
            school.Trainers = schoolView.Trainers;
            school.Students = schoolView.Students;
            if (ModelState.IsValid)
            {
                this._schoolRepo.Update(school);
                Repos.DbHundler.Save();
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
            Repos.DbHundler.Save();
            return RedirectToAction("Details", "Schools", new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this._schoolRepo.Dispose();
                //Repos.courseRepo.Dispose();
                //Repos.assignmentRepo.Dispose();
                //Repos.trainerRepo.Dispose();
                //Repos.studentRepo.Dispose();
                //Repos.DbHundler.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
