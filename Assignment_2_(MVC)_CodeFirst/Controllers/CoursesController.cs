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
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View(Globals.courseRepo.GetAll());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = Globals.courseRepo.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            course.Students = Globals.courseRepo.GetStudents(course.ID);
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var trainers = new SelectList(Globals.trainerRepo.GetAll(), "ID", "FullName");
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

            CourseViewModel courseView = new CourseViewModel()
            {
                SelectedStudents = new List<int>(),
                Students = studentsSelectListItems,
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
            course.School = Globals.schoolRepo.Get(courseView.SchoolId);
            course.Trainer = Globals.trainerRepo.Get(courseView.TrainerId);
            if (courseView.SelectedStudents != null)
            {
                var students = Globals.studentRepo.GetAll();
                foreach (var id in courseView.SelectedStudents)
                {
                    var selectedStudent = Globals.studentRepo.Get(id);
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
                Globals.courseRepo.Add(course);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = course.School.ID });
            }

            return View(courseView);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = Globals.courseRepo.Get(id);
            if (course == null)
                return HttpNotFound();

            course.Students = Globals.courseRepo.GetStudents(course.ID);

            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            var trainers = new SelectList(Globals.trainerRepo.GetAll(), "ID", "FullName");
            var selectedTrainer = trainers.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedTrainer != null) selectedTrainer.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
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
            Course courseDB = Globals.courseRepo.Get(courseView.ID);
            courseDB.Title = courseView.Title;
            courseDB.StartDate = courseView.StartDate;
            courseDB.EndDate = courseView.EndDate;
            courseDB.School = Globals.schoolRepo.Get(courseView.SchoolId);
            courseDB.Trainer = Globals.trainerRepo.Get(courseView.TrainerId);
            if(courseView.SelectedStudents != null)
            {
                var students = Globals.studentRepo.GetAll();
                foreach(var id in courseView.SelectedStudents)
                {
                    var selectedStudent = Globals.studentRepo.Get(id);
                    if (students.Contains(selectedStudent))
                        courseDB.Students.Add(selectedStudent);
                }
            }

            if (ModelState.IsValid)
            {
                Globals.courseRepo.Update(courseDB);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = courseDB.School.ID });
            }

            courseDB.Students = Globals.courseRepo.GetStudents(courseDB.ID);

            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == courseDB.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            var trainers = new SelectList(Globals.trainerRepo.GetAll(), "ID", "FullName");
            var selectedTrainer = trainers.FirstOrDefault(x => int.Parse(x.Value) == courseDB.ID);
            if (selectedTrainer != null) selectedTrainer.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
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
            Course course = Globals.courseRepo.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            course.Students = Globals.courseRepo.GetStudents(course.ID);
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = Globals.courseRepo.Get(id);
            Globals.courseRepo.Delete(course);
            Globals.DbHundler.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveStudent(int? courseId, int? studentId)
        {
            if (studentId == null || courseId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = Globals.courseRepo.Get(courseId);
            course.Students = Globals.courseRepo.GetStudents(course.ID);
            Student courseStudent = Globals.studentRepo.Get(studentId);
            if (course == null ||courseStudent == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            course.Students.Remove(courseStudent);

            if (ModelState.IsValid)
            {
                Globals.courseRepo.Update(course);
                Globals.DbHundler.Save();
                return RedirectToAction("Details", "Schools", new { id = course.School.ID });
            }

            course.Students = Globals.courseRepo.GetStudents(course.ID);
            var schools = new SelectList(Globals.schoolRepo.GetAll(), "ID", "Name");
            var selectedSchool = schools.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedSchool != null) selectedSchool.Selected = true;

            var trainers = new SelectList(Globals.trainerRepo.GetAll(), "ID", "FullName");
            var selectedTrainer = trainers.FirstOrDefault(x => int.Parse(x.Value) == course.ID);
            if (selectedTrainer != null) selectedTrainer.Selected = true;

            List<SelectListItem> studentsSelectListItems = new List<SelectListItem>();
            foreach (Student student in Globals.studentRepo.GetAll())
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = student.FullName,
                    Value = student.ID.ToString()
                };
                if (!course.Students.Contains(student))
                    studentsSelectListItems.Add(selectList);
            }

            CourseViewModel courseView2 = new CourseViewModel()
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

            return View(courseView2);
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
