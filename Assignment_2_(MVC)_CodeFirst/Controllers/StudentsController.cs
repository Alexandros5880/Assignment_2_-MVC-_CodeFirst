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
    public class StudentsController : Controller
    {
        private Repos _repositories;
        private SchoolRepo _schoolRepo;
        private StudentRepo _studentRepo;
        private CourseRepo _courseRepo;
        private AssignmentRepo _assignmentRepo;
        public StudentsController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._schoolRepo = this._repositories.Schools;
            this._studentRepo = this._repositories.Students;
            this._courseRepo = this._repositories.Courses;
            this._assignmentRepo = this._repositories.Assignments;
        }
        // GET: Students
        public ActionResult Index()
        {
            return View(this._studentRepo.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Search)
        {
            if (Search != null && Search.Length > 0)
                return View(this._studentRepo.GetAllByName(Search));
            else
                return View(this._studentRepo.GetAll());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this._studentRepo.Get(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
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
            List<SelectListItem> assignmentSelectListItems = new List<SelectListItem>();
            foreach (Assignment assignment in this._assignmentRepo.GetAllBySchool(schoolId))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = assignment.Title,
                    Value = assignment.ID.ToString()
                };
                assignmentSelectListItems.Add(selectList);
            }
            StudentViewModel studentView = new StudentViewModel()
            {
                SelectedCourses = new List<int>(),
                SelectedAssignments = new List<int>(),
                Courses = coursesSelectListItems,
                Assignments = assignmentSelectListItems,
                Schools = schools,
                SchoolId = schoolId
            };
            return View(studentView);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel studentView)
        {
            Student student = new Student();
            student.FirstName = studentView.FirstName;
            student.LastName = studentView.LastName;
            student.StartDate = studentView.StartDate;
            student.School = this._schoolRepo.Get(studentView.SchoolId);
            if (studentView.SelectedCourses != null)
            {
                var courses = this._courseRepo.GetAllBySchool(student.School.ID);
                foreach (var id in studentView.SelectedCourses)
                {
                    var selectedCourse = this._courseRepo.Get(id);
                    if (courses.Contains(selectedCourse))
                    { 
                        if (student.Courses != null)
                        {
                            student.Courses.Add(selectedCourse);
                        } 
                        else
                        {
                            student.Courses = new List<Course>();
                            student.Courses.Add(selectedCourse);
                        }
                    }
                        
                }
            }
            if (studentView.SelectedAssignments != null)
            {
                var assignments = this._assignmentRepo.GetAllBySchool(student.School.ID);
                foreach (var id in studentView.SelectedAssignments)
                {
                    var selectedAssignment = this._assignmentRepo.Get(id);
                    if (assignments.Contains(selectedAssignment))
                    {
                        if (student.Assignments != null)
                        {
                            student.Assignments.Add(selectedAssignment);
                        }
                        else
                        {
                            student.Assignments = new List<Assignment>();
                            student.Assignments.Add(selectedAssignment);
                        }
                    }
                        
                }
            }
            if (ModelState.IsValid)
            {
                this._studentRepo.Add(student);
                this._studentRepo.Save();
                return RedirectToAction("Details", "Schools", new { id = student.School.ID });
            }
            return View(studentView);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = this._studentRepo.Get(id);
            if (student == null)
                return HttpNotFound();

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == student.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach( Course course in this._courseRepo.GetAllBySchool(student.School.ID) )
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.ID.ToString()
                };
                if (!student.Courses.Contains(course))
                    coursesSelectListItems.Add(selectList);
            }

            List<SelectListItem> assignmentSelectListItems = new List<SelectListItem>();
            foreach (Assignment assignment in this._assignmentRepo.GetAllBySchool(student.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = assignment.Title,
                    Value = assignment.ID.ToString()
                };
                if(!student.Assignments.Contains(assignment))    
                    assignmentSelectListItems.Add(selectList);
            }

            StudentViewModel studentView = new StudentViewModel()
            {
                ID = student.ID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                StartDate = student.StartDate,
                SchoolId = student.School.ID,
                SelectedCourses = new List<int>(),
                SelectedAssignments = new List<int>(),
                Courses = coursesSelectListItems,
                Assignments = assignmentSelectListItems,
                Schools = schools,
                MyCourses = student.Courses,
                MyAssignments = student.Assignments
            };
            return View(studentView);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel studentView)
        {
            Student studentDB = this._studentRepo.Get(studentView.ID);
            studentDB.FirstName = studentView.FirstName;
            studentDB.LastName = studentView.LastName;
            studentDB.StartDate = studentView.StartDate;
            studentDB.School = this._schoolRepo.Get(studentView.SchoolId);
            if(studentView.SelectedCourses != null)
            {
                var courses = this._courseRepo.GetAll();
                foreach (var id in studentView.SelectedCourses)
                {
                    var selectedCourse = this._courseRepo.Get(id);
                    if (courses.Contains(selectedCourse))
                        studentDB.Courses.Add(selectedCourse);
                }
            }
            if (studentView.SelectedAssignments != null)
            {
                var assignments = this._assignmentRepo.GetAll();
                foreach (var id in studentView.SelectedAssignments)
                {
                    var selectedAssignment = this._assignmentRepo.Get(id);
                    if (assignments.Contains(selectedAssignment))
                        studentDB.Assignments.Add(selectedAssignment);
                }
            }

            if (ModelState.IsValid)
            {
                this._studentRepo.Update(studentDB);
                this._studentRepo.Save();
                return RedirectToAction("Details", "Schools", 
                    new { id = studentDB.School.ID });
            }

            var schools = new SelectList(this._schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == studentDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach (Course course in this._courseRepo.GetAllBySchool(studentDB.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.ID.ToString()
                };
                if (!studentDB.Courses.Contains(course))
                    coursesSelectListItems.Add(selectList);
            }

            List<SelectListItem> assignmentSelectListItems = new List<SelectListItem>();
            foreach (Assignment assignment in this._assignmentRepo.GetAllBySchool(studentDB.School.ID))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = assignment.Title,
                    Value = assignment.ID.ToString()
                };
                if (!studentDB.Assignments.Contains(assignment))
                    assignmentSelectListItems.Add(selectList);
            }

            StudentViewModel studentView2 = new StudentViewModel()
            {
                ID = studentDB.ID,
                FirstName = studentDB.FirstName,
                LastName = studentDB.LastName,
                StartDate = studentDB.StartDate,
                SchoolId = studentDB.School.ID,
                SelectedCourses = new List<int>(),
                SelectedAssignments = new List<int>(),
                Courses = coursesSelectListItems,
                Assignments = assignmentSelectListItems,
                Schools = schools,
                MyCourses = studentDB.Courses,
                MyAssignments = studentDB.Assignments
            };
            return View(studentView2);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this._studentRepo.Get(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = this._studentRepo.Get(id);
            this._studentRepo.Delete(student);
            this._studentRepo.Save();
            return RedirectToAction("Details", "Schools",
                    new { id = student.School.ID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repositories.Dispose();
                this._schoolRepo.Dispose();
                this._studentRepo.Dispose();
                this._courseRepo.Dispose();
                this._assignmentRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
