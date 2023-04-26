using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GradManSystem1.Controllers
{
    public class ProffesorsController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public ProffesorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public void PopulateCoursesDropDownList(object selectedCourse = null)
        {
            var coursesQuery = from d in _context.Courses
                               orderby d.Name // Sort by name.
                               select d;

            ViewBag.Courses = new SelectList(coursesQuery.AsNoTracking(),
                                         nameof(GradManSystem1.Models.Courses.Id),
                                              nameof(GradManSystem1.Models.Courses.Name),
                                              selectedCourse);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Department
                                   orderby d.Name
                                   select d;

            ViewBag.Departments = new SelectList(departmentsQuery.AsNoTracking(), "Id", "Name", selectedDepartment);
        }

        // GET: Proffesors
        [Authorize(Roles = "Admin,Profesor")]
        public async Task<IActionResult> Index()
        {
            //var model = new Proffesor();
            //model.Department.ToList();
            //return View(model);
            //PopulateCoursesDropDownList();

            return View(await _context.Proffesor.ToListAsync());
        }



        public IActionResult AfishoTeDhena()
        {
            return View();
        }




        // GET: Proffesors/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proffesor == null)
            {
                return NotFound();
            }

            var proffesor = await _context.Proffesor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proffesor == null)
            {
                return NotFound();
            }

            return View(proffesor);
        }

        // GET: Proffesors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulateCoursesDropDownList();
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Proffesors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Courses,DepartmentId")] Proffesor proffesor)
        {
            if (ModelState.IsValid)
            {
                proffesor.DateCreated = DateTime.UtcNow;
                proffesor.IsDeleted = false;
                _context.Add(proffesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proffesor);
        }

        // GET: Proffesors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proffesor == null)
            {
                return NotFound();
            }
            var proffesor = await _context.Proffesor.FindAsync(id);
            if (proffesor == null)
            {
                return NotFound();
            }
            return View(proffesor);
        }

        // POST: Proffesors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Email")] Proffesor proffesor)
        {
            if (id != proffesor.Id)
            {
                proffesor.DateUpdated = DateTime.UtcNow;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    proffesor.DateUpdated = DateTime.UtcNow;
                    _context.Update(proffesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProffesorExists(proffesor.Id))
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
            return View(proffesor);
        }

        // GET: Proffesors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proffesor == null)
            {
                return NotFound();
            }

            var proffesor = await _context.Proffesor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proffesor == null)
            {
                return NotFound();
            }

            return View(proffesor);
        }

        // POST: Proffesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proffesor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Proffesor'  is null.");
            }
            var proffesor = await _context.Proffesor.FindAsync(id);
            if (proffesor != null)
            {
                //proffesor.IsDeleted=true;
                //_context.Update(proffesor);
                _context.Proffesor.Remove(proffesor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProffesorExists(int id)
        {
            return _context.Proffesor.Any(e => e.Id == id);
        }
    }
}
