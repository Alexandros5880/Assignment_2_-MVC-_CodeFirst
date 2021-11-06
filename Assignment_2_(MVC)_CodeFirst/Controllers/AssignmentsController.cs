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
    public class AssignmentsController : Controller
    {
        private Repos _repositories;
        private SchoolRepo _schoolRepo;
        private AssignmentRepo _assignmentRepo;
        private StudentRepo _studentRepo;
        public AssignmentsController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._schoolRepo = this._repositories.Schools;
            this._assignmentRepo = this._repositories.Assignments;
            this._studentRepo = this._repositories.Students;
        }
        // GET: Assignments
        public ActionResult Index()
        {
            return View(this._assignmentRepo.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Search)
        {
            if (Search != null && Search.Length > 0)
                return View(this._assignmentRepo.GetAllByName(Search));
            else
                return View(this._assignmentRepo.GetAll());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = this._assignmentRepo.Get(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create(int schoolId)
        {
            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");

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

            AssignmentViewModel assignmentView = new AssignmentViewModel()
            {
                SelectedStudents = new List<int>(),
                Students = studentsSelectListItems,
                Schools = schools,
                SchoolId= schoolId
            };

            return View(assignmentView);
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignmentViewModel assignmentView)
        {
            Assignment assignment = this._assignmentRepo.Get(assignmentView.ID);
            assignment.Title = assignmentView.Title;
            assignment.StartDate = assignmentView.StartDate;
            assignment.EndDate = assignmentView.EndDate;
            assignment.School = this._schoolRepo.Get(assignmentView.SchoolId);
            if (assignmentView.SelectedStudents != null)
            {
                var students = this._studentRepo.GetAllBySchool(assignment.School.ID);
                foreach (var id in assignmentView.SelectedStudents)
                {
                    var student = this._studentRepo.Get(id);
                    if (students.Contains(student))
                    {
                        if (assignment.Students != null)
                        {
                            assignment.Students.Add(student);
                        }
                        else
                        {
                            assignment.Students = new List<Student>();
                            assignment.Students.Add(student);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                this._assignmentRepo.Add(assignment);
                this._assignmentRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = assignment });
            }

            return View(assignmentView);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Assignment assignment = this._assignmentRepo.Get(id);
            if (assignment == null)
                return HttpNotFound();
            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == assignment.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;
            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in this._studentRepo.GetAllBySchool(assignment.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!assignment.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }
            AssignmentViewModel assignmentView = new AssignmentViewModel()
            {
                ID = assignment.ID,
                Title = assignment.Title,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                SelectedStudents = new List<int>(),
                Students = studentsSelectListItems,
                MyStudents = assignment.Students,
                SchoolId = assignment.School.ID,
                Schools = schools
            };
            return View(assignmentView);
        }

        // POST: Assignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssignmentViewModel assignmentView)
        {
            Assignment assignmentDB = this._assignmentRepo.Get(assignmentView.ID);
            assignmentDB.Title = assignmentView.Title;
            assignmentDB.StartDate = assignmentView.StartDate;
            assignmentDB.EndDate = assignmentView.EndDate;
            assignmentDB.School = this._schoolRepo.Get(assignmentView.SchoolId);
            if (assignmentView.SelectedStudents != null)
            {
                var students = this._studentRepo.GetAllBySchool(assignmentDB.School.ID);
                foreach(var id in assignmentView.SelectedStudents)
                {
                    var student = this._studentRepo.Get(id);
                    if (students.Contains(student))
                        assignmentDB.Students.Add(student);
                }
            }
            if (ModelState.IsValid)
            {
                this._assignmentRepo.Update(assignmentDB);
                this._assignmentRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = assignmentDB.School.ID });
            }
            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == assignmentDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;
            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in this._studentRepo.GetAllBySchool(assignmentDB.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!assignmentDB.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }
            AssignmentViewModel assignmentView2 = new AssignmentViewModel()
            {
                ID = assignmentDB.ID,
                Title = assignmentDB.Title,
                StartDate = assignmentDB.StartDate,
                EndDate = assignmentDB.EndDate,
                SelectedStudents = new List<int>(),
                Students = studentsSelectListItems,
                MyStudents = assignmentDB.Students,
                SchoolId = assignmentDB.School.ID,
                Schools = schools
            };
            return View(assignmentView2);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = this._assignmentRepo.Get(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = this._assignmentRepo.Get(id);
            this._assignmentRepo.Delete(assignment);
            this._assignmentRepo.Save();
            return RedirectToAction("Details", "Schools", new { id = assignment.School.ID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repositories.Dispose();
                this._schoolRepo.Dispose();
                this._assignmentRepo.Dispose();
                this._studentRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
