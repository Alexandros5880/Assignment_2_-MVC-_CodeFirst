using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using Assignment_2__MVC__CodeFirst.ViewModels;

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class StudentsController : Controller
    {

        // GET: Students
        public ActionResult Index()
        {
            return View(Globals.studentRepo.GetAll());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Globals.studentRepo.Get(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            return View(new Student());
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,StartDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                Globals.studentRepo.Add(student);
                Globals.DbHundler.Save();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Globals.studentRepo.Get(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == student.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach( Course course in Globals.courseRepo.GetAll() )
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
            foreach (Assignment assignment in Globals.assignmentRepo.GetAll())
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
                SchoolID = student.School.ID,
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
            Student studentDB = Globals.studentRepo.Get(studentView.ID);
            studentDB.FirstName = studentView.FirstName;
            studentDB.LastName = studentView.LastName;
            studentDB.StartDate = studentView.StartDate;
            studentDB.School = Globals.schoolRepo.Get(studentView.SchoolID);
            studentDB.Courses.Clear();
            var courses = Globals.courseRepo.GetAll();
            foreach (var id in studentView.SelectedCourses)
            {
                var selectedCourse = Globals.courseRepo.Get(id);
                if (courses.Contains(selectedCourse)) 
                    studentDB.Courses.Add(selectedCourse);
            }
            studentDB.Assignments.Clear();
            var assignments = Globals.assignmentRepo.GetAll();
            foreach (var id in studentView.SelectedAssignments)
            {
                var selectedAssignment = Globals.assignmentRepo.Get(id);
                if (assignments.Contains(selectedAssignment))
                    studentDB.Assignments.Add(selectedAssignment);
            }

            string stdata = $@"ID: {studentDB.ID}  
                               Name: {studentDB.FullName}  
                               School: {studentDB.School}
                               StartDate: {studentDB.StartDate}";

            if (ModelState.IsValid)
            {
                Globals.studentRepo.Update(studentDB);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = studentDB.School.ID });
            }

            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == studentDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> coursesSelectListItems = new List<SelectListItem>();
            foreach (Course course in Globals.courseRepo.GetAll())
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
            foreach (Assignment assignment in Globals.assignmentRepo.GetAll())
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
                SchoolID = studentDB.School.ID,
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
            Student student = Globals.studentRepo.Get(id);
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
            Student student = Globals.studentRepo.Get(id);
            Globals.studentRepo.Delete(student);
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
            base.Dispose(disposing);
        }
    }
}
