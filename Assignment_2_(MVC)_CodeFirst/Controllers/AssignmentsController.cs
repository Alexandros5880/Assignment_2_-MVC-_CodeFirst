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
using Assignment_2__MVC__CodeFirst.Static;
using Assignment_2__MVC__CodeFirst.ViewModels;

namespace Assignment_2__MVC__CodeFirst.Controllers
{
    public class AssignmentsController : Controller
    {

        // GET: Assignments
        public ActionResult Index()
        {
            return View(Globals.assignmentRepo.GetAll());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = Globals.assignmentRepo.Get(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
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
                Schools = schools
            };

            return View(assignmentView);
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignmentViewModel assignmentView)
        {
            Assignment assignment = Globals.assignmentRepo.Get(assignmentView.ID);
            assignment.Title = assignmentView.Title;
            assignment.StartDate = assignmentView.StartDate;
            assignment.EndDate = assignmentView.EndDate;
            assignment.School = Globals.schoolRepo.Get(assignmentView.SchoolId);
            if (assignmentView.SelectedStudents != null)
            {
                var students = Globals.studentRepo.GetAll();
                foreach (var id in assignmentView.SelectedStudents)
                {
                    var student = Globals.studentRepo.Get(id);
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
                Globals.assignmentRepo.Add(assignment);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = assignment });
            }

            return View(assignmentView);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Assignment assignment = Globals.assignmentRepo.Get(id);
            if (assignment == null)
                return HttpNotFound();

            assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == assignment.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
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
            Assignment assignmentDB = Globals.assignmentRepo.Get(assignmentView.ID);
            assignmentDB.Title = assignmentView.Title;
            assignmentDB.StartDate = assignmentView.StartDate;
            assignmentDB.EndDate = assignmentView.EndDate;
            assignmentDB.School = Globals.schoolRepo.Get(assignmentView.SchoolId);
            if (assignmentView.SelectedStudents != null)
            {
                var students = Globals.studentRepo.GetAll();
                foreach(var id in assignmentView.SelectedStudents)
                {
                    var student = Globals.studentRepo.Get(id);
                    if (students.Contains(student))
                        assignmentDB.Students.Add(student);
                }
            }

            if (ModelState.IsValid)
            {
                Globals.assignmentRepo.Update(assignmentDB);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = assignmentDB.School.ID });
            }

            assignmentDB.Students = Globals.assignmentRepo.GetStudents(assignmentDB.ID);
            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == assignmentDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
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
            Assignment assignment = Globals.assignmentRepo.Get(id);
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
            Assignment assignment = Globals.assignmentRepo.Get(id);
            assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            Globals.assignmentRepo.Delete(assignment);
            Globals.DbHundler.Save();
            return RedirectToAction("Details", "Schools", new { id = assignment.School.ID });
        }

        [HttpPost]
        public ActionResult RemoveStudent(int? assignmentId, int? studentId)
        {
            if (assignmentId == null || studentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Assignment assignment = Globals.assignmentRepo.Get(assignmentId);
            assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            Student assignmentStudent = Globals.studentRepo.Get(studentId);
            if (assignment == null || assignmentStudent == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            assignment.Students.Remove(assignmentStudent);

            if (ModelState.IsValid)
            {
                Globals.assignmentRepo.Update(assignment);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = assignmentStudent.School.ID });
            }

            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == assignment.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!assignment.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }

            AssignmentViewModel assignmentView2 = new AssignmentViewModel()
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

            return View(assignmentView2);
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
