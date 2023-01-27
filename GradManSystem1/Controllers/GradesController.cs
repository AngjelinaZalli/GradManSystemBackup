using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GradManSystem1.Controllers
{
    

    public class GradesController : Controller
    {
        //private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        //public GradesController(IUserService service)
        //{
        //    _userService = service;
        //}

        //public void CreateGrade(Grade grade)
        //{
        //    var UserId = _userService.getUserId();
        //    grade.UserId = UserId;

        //}



        public GradesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        private void PopulateStudentsDropDownList(object selectedStudents = null)
        {
            var StudentsQuery = from d in _context.Student
                                orderby d.Name
                                select d;

            ViewBag.Students = new SelectList(StudentsQuery.AsNoTracking(), "Id", "Name", selectedStudents);
        }

        private void PopulateCoursesDropDownList(object selectedCourses = null)
        {
            var CoursesQuery = from d in _context.Courses
                               orderby d.Name
                               select d;

            ViewBag.Courses = new SelectList(CoursesQuery.AsNoTracking(), "Id", "Name", selectedCourses);
        }

        private void PopulateSProffesorsDropDownList(object selectedProffesors = null)
        {
            var ProffesorsQuery = from d in _context.Proffesor
                                  orderby d.Name
                                  select d;

            ViewBag.Proffesors = new SelectList(ProffesorsQuery.AsNoTracking(), "Id", "Name", selectedProffesors);
        }



        // GET: Grades
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grade.ToListAsync());
        }
        
        [Authorize(Roles = "Profesor,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }


        // GET: Grades/Create
        [Authorize(Roles = "Admin,Profesor")]
        public IActionResult Create()
        {
            PopulateStudentsDropDownList();
            PopulateCoursesDropDownList();
            PopulateSProffesorsDropDownList();
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Create([Bind("GradeMark,StudentId,CoursesId,ProffesorId")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.DateCreated = DateTime.UtcNow;
                grade.IsDeleted = false;
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Edit/5
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Edit(int id, [Bind("GradeMark,Id")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    grade.DateUpdated = DateTime.UtcNow;
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Delete/5

        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grade == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grade'  is null.");
            }
            var grade = await _context.Grade.FindAsync(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.Id == id);
        }
    }
}
