using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.Static;
using Assignment_2__MVC__CodeFirst.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class TrainersController : Controller
    {
        private Repos _repositories;
        private SchoolRepo _schoolRepo;
        private TrainerRepo _trainerRepo;
        private CourseRepo _courseRepo;
        public TrainersController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._schoolRepo = this._repositories.Schools;
            this._trainerRepo = this._repositories.Trainers;
            this._courseRepo = this._repositories.Courses;
        }
        // GET: Trainers
        public ActionResult Index()
        {
            return View(this._trainerRepo.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Search)
        {
            if (Search != null && Search.Length > 0)
                return View(this._trainerRepo.GetAllByName(Search));
            else
                return View(this._trainerRepo.GetAll());
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = this._trainerRepo.Get(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainers/Create
        public ActionResult Create(int schoolId)
        {
            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach (Course course in this._courseRepo.GetAllBySchool(schoolId))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.ID.ToString()
                };
                coursesSelectListItems.Add(selectList);
            }
            TrainerViewModel trainerView = new TrainerViewModel()
            {
                SelectedCourses = new List<int>(),
                Courses = coursesSelectListItems,
                Schools = schools,
                SchoolId = schoolId
            };
            return View(trainerView);
        }

        // POST: Trainers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerViewModel trainerView)
        {
            Trainer trainer = new Trainer();
            trainer.FirstName = trainerView.FirstName;
            trainer.LastName = trainerView.LastName;
            trainer.StartDate = trainerView.StartDate;
            trainer.School = this._schoolRepo.Get(trainerView.SchoolId);
            if (trainerView.SelectedCourses != null)
            {
                var courses = this._courseRepo.GetAllBySchool(trainer.School.ID);
                foreach (var id in trainerView.SelectedCourses)
                {
                    var selectedCourse = this._courseRepo.Get(id);
                    if (courses.Contains(selectedCourse))
                    {
                        if (trainer.Courses != null)
                        {
                            trainer.Courses.Add(selectedCourse);
                        }
                        else
                        {
                            trainer.Courses = new List<Course>();
                            trainer.Courses.Add(selectedCourse);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                this._trainerRepo.Add(trainer);
                this._trainerRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = trainer.School.ID });
            }

            return View(trainerView);
        }

        // GET: Trainers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Trainer trainer = this._trainerRepo.Get(id);
            if (trainer == null)
                return HttpNotFound();

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == trainer.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach (Course course in this._courseRepo.GetAllBySchool(trainer.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.ID.ToString()
                };
                if (!trainer.Courses.Contains(course))
                    coursesSelectListItems.Add(selectList);
            }

            TrainerViewModel trainerView = new TrainerViewModel()
            {
                ID = trainer.ID,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                StartDate = trainer.StartDate,
                SchoolId = trainer.School.ID,
                SelectedCourses = new List<int>(),
                Courses = coursesSelectListItems,
                Schools = schools,
                MyCourses = trainer.Courses
            };

            return View(trainerView);
        }

        // POST: Trainers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainerViewModel trainerView)
        {
            Trainer trainerDB = this._trainerRepo
                .Get(trainerView.ID);
            trainerDB.FirstName = trainerView.FirstName;
            trainerDB.LastName = trainerView.LastName;
            trainerDB.StartDate = trainerView.StartDate;
            trainerDB.School = this._schoolRepo
                .Get(trainerView.SchoolId);
            if (trainerView.SelectedCourses != null)
            {
                var courses = this._courseRepo.GetAllBySchool(trainerDB.School.ID);
                foreach (var id in trainerView.SelectedCourses)
                {
                    var selectedCourse = this._courseRepo.Get(id);
                    if (courses.Contains(selectedCourse))
                        trainerDB.Courses.Add(selectedCourse);
                }
            }

            if (ModelState.IsValid)
            {
                this._trainerRepo.Update(trainerDB);
                this._trainerRepo.Save();
                return RedirectToAction("Details", "Schools", 
                    new { id = trainerDB.School.ID });
            }

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == trainerDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach (Course course in this._courseRepo.GetAllBySchool(trainerDB.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.ID.ToString()
                };
                if (!trainerDB.Courses.Contains(course))
                    coursesSelectListItems.Add(selectList);
            }

            TrainerViewModel trainerView2 = new TrainerViewModel()
            {
                ID = trainerDB.ID,
                FirstName = trainerDB.FirstName,
                LastName = trainerDB.LastName,
                StartDate = trainerDB.StartDate,
                SchoolId = trainerDB.School.ID,
                SelectedCourses = new List<int>(),
                Courses = coursesSelectListItems,
                Schools = schools,
                MyCourses = trainerDB.Courses
            };

            return View(trainerView2);
        }

        // GET: Trainers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = this._trainerRepo.Get(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = this._trainerRepo.Get(id);
            this._trainerRepo.Delete(trainer);
            this._trainerRepo.Save();
            return RedirectToAction("Details", "Schools",
                    new { id = trainer.School.ID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this._schoolRepo.Dispose();
                //this._courseRepo.Dispose();
                //this._assignmentRepo.Dispose();
                //this._trainerRepo.Dispose();
                //this._studentRepo.Dispose();
                //this._trainerRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
