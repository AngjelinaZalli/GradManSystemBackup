using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GradManSystem1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
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

        //private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        //{
        //    var departmentsQuery = from d in _context.Department
        //                           orderby d.Name
        //                           select d;

        //    ViewBag.Departments = new SelectList(departmentsQuery.AsNoTracking(), "Id", "Name", selectedDepartment);
        //}





        //public void PopulateProffesorsDropDownList(object selectedProffesors = null)
        //{
        //    var ProffesorsQuery = from d in _context.Proffesor
        //                          orderby d.Name // Sort by name.
        //                       select d;

        //    ViewBag.Proffesors = new SelectList(ProffesorsQuery.AsNoTracking(),
        //                                 nameof(GradManSystem1.Models.Proffesor.Id),
        //                                      nameof(GradManSystem1.Models.Proffesor.Name),
        //                                      selectedProffesors);
        //}





        // GET: Departments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Department.ToListAsync();
            return View(departments);
        }

        // GET: Departments/Details/5
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulateCoursesDropDownList();
            //PopulateProffesorsDropDownList();

            //PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NumberOfCourses,Image,Id,Courses,Proffesors")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.DateCreated = DateTime.UtcNow;
                department.IsDeleted = false;
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,NumberOfCourses,Image,Id")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    department.DateUpdated = DateTime.UtcNow;
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Department == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Department'  is null.");
            }
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
