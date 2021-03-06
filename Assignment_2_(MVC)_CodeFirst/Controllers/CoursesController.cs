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
    public class CoursesController : Controller
    {
        private Repos _repositories;
        private CourseRepo _courseRepo;
        private SchoolRepo _schoolRepo;
        private TrainerRepo _trainerRepo;
        private StudentRepo _studentRepo;
        public CoursesController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._courseRepo = this._repositories.Courses;
            this._schoolRepo = this._repositories.Schools;
            this._trainerRepo = this._repositories.Trainers;
            this._studentRepo = this._repositories.Students;
        }
        // GET: Courses
        public ActionResult Index()
        {
            return View(this._courseRepo.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Search)
        {
            if (Search != null && Search.Length > 0)
                return View(this._courseRepo.GetAllByName(Search));
            else
                return View(this._courseRepo.GetAll());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = this._courseRepo.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create(int schoolId)
        {
            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var trainers = new SelectList(this._trainerRepo.GetAllBySchool(schoolId), "ID", "FullName");
            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in this._studentRepo.GetAllBySchool(schoolId))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                studentsSelectListItems.Add(selectList);
            }

            CourseViewModel courseView = new CourseViewModel()
            {
                SelectedStudents = new List<int>(),
                Students = studentsSelectListItems,
                SchoolId = schoolId,
                Schools = schools,
                Trainers = trainers
            };

            return View(courseView);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel courseView)
        {
            Course course = new Course();
            course.Title = courseView.Title;
            course.StartDate = courseView.StartDate;
            course.EndDate = courseView.EndDate;
            course.School = this._schoolRepo.Get(courseView.SchoolId);
            course.Trainer = this._trainerRepo.Get(courseView.TrainerId);
            if (courseView.SelectedStudents != null)
            {
                var students = this._studentRepo.GetAllBySchool(course.School.ID);
                foreach (var id in courseView.SelectedStudents)
                {
                    var selectedStudent = this._studentRepo.Get(id);
                    if (students.Contains(selectedStudent))
                    {
                        if (course.Students != null)
                        {
                            course.Students.Add(selectedStudent);
                        }
                        else
                        {
                            course.Students = new List<Student>();
                            course.Students.Add(selectedStudent);
                        }
                    } 
                }
            }

            if (ModelState.IsValid)
            {
                this._courseRepo.Add(course);
                this._courseRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = course.School.ID });
            }

            return View(courseView);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = this._courseRepo.Get(id);
            if (course == null)
                return HttpNotFound();

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            var trainers = new SelectList(this._trainerRepo.GetAllBySchool(course.School.ID), "ID", "FullName");
            var selectedTrainer = trainers.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedTrainer != null) selectedTrainer.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in this._studentRepo.GetAllBySchool(course.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!course.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }

            CourseViewModel courseView = new CourseViewModel()
            {
                ID = course.ID,
                Title = course.Title,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                SchoolId = course.School.ID,
                TrainerId = course.Trainer.ID,
                SelectedStudents = new List<int>(),
                MyStudents = course.Students,
                Students = studentsSelectListItems,
                Schools = schools,
                Trainers = trainers
            };

            return View(courseView);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel courseView)
        {
            Course courseDB = this._courseRepo.Get(courseView.ID);
            courseDB.Title = courseView.Title;
            courseDB.StartDate = courseView.StartDate;
            courseDB.EndDate = courseView.EndDate;
            courseDB.School = this._schoolRepo.Get(courseView.SchoolId);
            courseDB.Trainer = this._trainerRepo.Get(courseView.TrainerId);
            if(courseView.SelectedStudents != null)
            {
                var students = this._studentRepo.GetAllBySchool(courseDB.School.ID);
                foreach(var id in courseView.SelectedStudents)
                {
                    var selectedStudent = this._studentRepo.Get(id);
                    if (students.Contains(selectedStudent))
                        courseDB.Students.Add(selectedStudent);
                }
            }

            if (ModelState.IsValid)
            {
                this._courseRepo.Update(courseDB);
                this._courseRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = courseDB.School.ID });
            }

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == courseDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            var trainers = new SelectList(this._trainerRepo.GetAllBySchool(courseDB.School.ID), "ID", "FullName");
            var selectedTrainer = trainers.FirstOrDefault(x => int.Parse(x.Value) == courseDB.ID);
            if (selectedTrainer != null) selectedTrainer.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in this._studentRepo.GetAllBySchool(courseDB.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!courseDB.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }

            CourseViewModel courseView2 = new CourseViewModel()
            {
                ID = courseDB.ID,
                Title = courseDB.Title,
                StartDate = courseDB.StartDate,
                EndDate = courseDB.EndDate,
                SchoolId = courseDB.School.ID,
                TrainerId = courseDB.Trainer.ID,
                SelectedStudents = new List<int>(),
                MyStudents = courseDB.Students,
                Students = studentsSelectListItems,
                Schools = schools,
                Trainers = trainers
            };

            return View(courseView2);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = this._courseRepo.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = this._courseRepo.Get(id);
            this._courseRepo.Delete(course);
            this._courseRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repositories.Dispose();
                this._courseRepo.Dispose();
                this._schoolRepo.Dispose();
                this._trainerRepo.Dispose();
                this._studentRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
