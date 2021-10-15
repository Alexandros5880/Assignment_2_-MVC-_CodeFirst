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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

            var courses = new SelectList(Globals.courseRepo.GetAll(), "ID", "Title");
            var selectedCourse = courses.FirstOrDefault(x => int.Parse(x.Value) == student.ID);
            if (selectedCourse != null) selectedCourse.Selected = true;

            StudentViewModel studentView = new StudentViewModel()
            {
                ID = student.ID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                StartDate = student.StartDate,
                SchoolID = student.School.ID,
                Courses = student.Courses,
                Assignments = student.Assignments,
                AllCourses = courses,
                AllSchools = schools
            };
            return View(studentView);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel studentView)
        {
            Student studentDB = Globals.studentRepo.Get(studentView.ID);
            studentDB.FirstName = studentView.FirstName;
            studentDB.LastName = studentView.LastName;
            studentDB.StartDate = studentView.StartDate;
            studentDB.School = Globals.schoolRepo.Get(studentView.SchoolID);
            studentDB.Courses = studentView.Courses;
            studentDB.Assignments = studentView.Assignments;

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

            var courses = new SelectList(Globals.courseRepo.GetAll(), "ID", "Title");
            var selectedCourse = courses.FirstOrDefault(x => int.Parse(x.Value) == studentDB.ID);
            if (selectedCourse != null) selectedCourse.Selected = true;

            StudentViewModel studentView2 = new StudentViewModel()
            {
                ID = studentDB.ID,
                FirstName = studentDB.FirstName,
                LastName = studentDB.LastName,
                StartDate = studentDB.StartDate,
                SchoolID = studentDB.School.ID,
                Courses = studentDB.Courses,
                Assignments = studentDB.Assignments,
                AllCourses = courses,
                AllSchools = schools
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
