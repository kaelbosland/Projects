using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mosaic.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mosaic.Services;

namespace Mosaic.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LoginSystemContext _context;
        private readonly IStudentAuthentication _service;

        public StudentsController(LoginSystemContext context, IStudentAuthentication service)
        {
            _context = context;
            _service = service;
        }


        //GET: Students/CannotEnroll
        public IActionResult CannotEnroll()
        {
            return View();
        }

        //GET: Students/EnrollInClass
        public IActionResult EnrollInClass()
        {
            return View();
        }

        //GET: Students/CannotDrop
        public IActionResult CannotDrop()
        {
            return View();
        }

        //GET: Students/DropClass
        public IActionResult DropClass()
        {
            return View();
        }

        //GET: Students/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        //POST: Students/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string old, string newPass)
        {
            var student = _service.VerifyChangePassword(HttpContext.Session.GetString("username"), old, newPass);

            if (student != null)
            {
                _context.Student.Update(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("LoginStudent");
        }

        // POST: Students/DropClass
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DropClass(string classCode)
        {
            if (_service.AllowDrop(classCode, HttpContext.Session.GetString("username")) == 1)
            {
                var chosenClass = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == classCode.ToUpper());
                var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == HttpContext.Session.GetString("username"));
                student.ClassOne = null;
                chosenClass.NumEnrolled--;
                _context.Student.Update(student);
                _context.Class.Update(chosenClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DropClass));
            }
            else if (_service.AllowDrop(classCode, HttpContext.Session.GetString("username")) == 2)
            {
                var chosenClass = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == classCode.ToUpper());
                var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == HttpContext.Session.GetString("username"));
                student.ClassTwo = null;
                chosenClass.NumEnrolled--;
                _context.Student.Update(student);
                _context.Class.Update(chosenClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DropClass));
            }
                    
            return RedirectToAction(nameof(CannotDrop));
        }

        // POST: Students/EnrollInClass
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollInClass (string classCode)
        {
            if (_service.AllowEnroll(classCode, HttpContext.Session.GetString("username")) == 1)
            {
                var chosenClass = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == classCode.ToUpper());
                var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == HttpContext.Session.GetString("username"));
                student.ClassOne = classCode.ToUpper();
                chosenClass.NumEnrolled++;
                _context.Student.Update(student);
                _context.Class.Update(chosenClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EnrollInClass));

            } else if (_service.AllowEnroll(classCode, HttpContext.Session.GetString("username")) == 2)
            {
                var chosenClass = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == classCode.ToUpper());
                var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == HttpContext.Session.GetString("username"));
                student.ClassTwo = classCode.ToUpper();
                chosenClass.NumEnrolled++;
                _context.Student.Update(student);
                _context.Class.Update(chosenClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EnrollInClass));
            }

            return RedirectToAction(nameof(CannotEnroll));
        }

        //GET: Home
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username").Equals(""))
            {
                return View();
            } else
            {
                return RedirectToAction("Edit");
            }
        }

        //GET: Students/UsernameTaken
        public IActionResult UsernameTaken()
        {
            return View();
        }

        //GET: Students/Menu
        public IActionResult Menu ()
        {
            return View();
        }

        public IActionResult Logout ()
        {
            HttpContext.Session.SetString("username", "");
            return RedirectToAction("Home");
        }

        //GET: Students/NotFound
        public IActionResult StudentNotFound()
        {
            return View();
        }
        
        // GET: Students
        public async Task<IActionResult> Index()
        {
            var loginSystemContext = _context.Student.Include(s => s.ClassOneNavigation).Include(s => s.ClassTwoNavigation);
            return View(await loginSystemContext.ToListAsync());
        }

              // GET: Students/CreateStudent
        public IActionResult CreateStudent()
        {
            HttpContext.Session.SetString("username", "");
            var items1 = _context.Class.ToList();
                var items2 = _context.Class.ToList();
                items1.Insert(0, new Class { ClassCode = "" });
                items2.Insert(0, new Class { ClassCode = "" });

                ViewData["ClassOne"] = new SelectList(items1, "ClassCode", "ClassCode", string.Empty);
                ViewData["ClassTwo"] = new SelectList(items2, "ClassCode", "ClassCode", string.Empty);
                return View();
        }

        // POST: Students/CreateStudent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent([Bind("Username,Password,FirstName,LastName,ClassOne,ClassTwo")] Student student)
        {
            HttpContext.Session.SetString("username", "");
            var items1 = _context.Class.ToList();
            var items2 = _context.Class.ToList();
            items1.Insert(0, new Class { ClassCode = "" });
            items2.Insert(0, new Class { ClassCode = "" });

            ViewData["ClassOne"] = new SelectList(items1, "ClassCode", "ClassCode", string.Empty);
            ViewData["ClassTwo"] = new SelectList(items2, "ClassCode", "ClassCode", string.Empty);

            string errorCode = _service.AllowCreate(student.Username, student.Password, student.ClassOne, student.ClassTwo);

            switch (errorCode) {
                case "":
                    var classOne = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == student.ClassOne);
                    var classTwo = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == student.ClassTwo);
                    if (classOne != null) { classOne.NumEnrolled++; _context.Update(classOne); } else { student.ClassOne = null; }
                    if (classTwo != null) { classTwo.NumEnrolled++; _context.Update(classTwo); } else { student.ClassTwo = null; }
                    student.Password = _service.EncryptPassword(student.Password);
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("StudentAdded");
                case "usernametaken":
                    TempData["ErrorMessage"] = "Username Taken";
                    return RedirectToAction("CannotCreateStudent");
                case "infotooshort":
                    TempData["ErrorMessage"] = "Your username and password must be at least 5 characters";
                    return RedirectToAction("CannotCreateStudent");
                case "c1Invalid":
                    TempData["ErrorMessage"] = student.ClassOne + " is either full or could not be found in database.";
                    return RedirectToAction("CannotCreateStudent");
                case "c2Invalid":
                    TempData["ErrorMessage"] = student.ClassTwo + " is either full or could not be found in database.";
                    return RedirectToAction("CannotCreateStudent");
                case "c1Invalidc2Invalid":
                    TempData["ErrorMessage"] = student.ClassOne + " and " + student.ClassTwo + " are full, or both could not be found in database.";
                    return RedirectToAction("CannotCreateStudent");
            }

            //code should never reach here
            return NotFound();
        }

        public IActionResult CannotCreateStudent()
        {
            return View();
        }

        // GET: Students/LoginStudent
        public IActionResult LoginStudent()
        {
            return View();
        }

        // POST: Students/LoginStudent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginStudent(string username, string password)
        {
            if (_service.AllowLogin(username, password))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction(nameof(Menu));
            } else
            {
                return RedirectToAction(nameof(StudentNotFound));
            }
        }

        public IActionResult StudentAdded()
        {
            return View();
        }

        // GET: Students/Edit
        public async Task<IActionResult> Edit()
        {
                String id = HttpContext.Session.GetString("username");
            
                if (id == null)
                {
                return NotFound();
                }

                var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == id);
                ViewData["Student"] = student;
                if (student == null)
                {
                    return RedirectToAction(nameof(StudentNotFound));
                }

            return View(student);
        }

        // POST: Students/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Username,Password,FirstName,LastName,ClassOne,ClassTwo")] Student student)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ViewData["Student"] = student;
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(student);
        }

        // GET: Students/Delete
        public async Task<IActionResult> Delete()
        {
            String id = HttpContext.Session.GetString("username");
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .SingleOrDefaultAsync(m => m.Username == id);
            if (student == null)
            {
                return RedirectToAction(nameof(StudentNotFound));
            }

            return View(student);
        }

        // POST: Students/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {

            String id = HttpContext.Session.GetString("username");
            var student = await _context.Student.SingleOrDefaultAsync(m => m.Username == id);
            if (student.ClassOne != null)
            {
                var classOne = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == student.ClassOne);
                classOne.NumEnrolled--;
                _context.Class.Update(classOne);
            }

            if (student.ClassTwo != null)
            {
                var classTwo = await _context.Class.SingleOrDefaultAsync(m => m.ClassCode == student.ClassTwo);
                classTwo.NumEnrolled--;
                _context.Class.Update(classTwo);
            }
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Home));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.Username == id);
        }
    }
}