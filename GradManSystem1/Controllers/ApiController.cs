using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;


namespace GradManSystem1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        //afisho studentet sipas vitit te lindjes
        [HttpGet]
        [Route("Birthday")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult Birthday()
        {
            var result = from Student in _context.Student
                         group Student by new { Student.Birthday.Year } into rezultat
                         select new
                         {
                             Viti = new DateTime(rezultat.Key.Year, 1, 1).ToString("yyyy"),
                             Count = rezultat.Count()
                         };
            return Ok(result);
        }
        //afisho departamentet sipas nr te kurseve
        [HttpGet]
        [Route("NumberOfCourses")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult NumberOfCourses()
        {
            var result = from Department in _context.Department
                         group Department by new { Department.NumberOfCourses } into rezultat
                         select new
                         {
                             Dep = rezultat.Key,
                             Count = rezultat.Count()
                         };
            return Ok(result);
        }
        //Merr nr perdoruesve
        [HttpGet]
        public IActionResult NumriPerdoruesve()
        {
            return Ok(_context.Student.Count());
        }
        //merr nr kurseve
        [HttpGet]
        public IActionResult NumriKurseve()
        {
            return Ok(_context.Courses.Count());
        }

        //tabele kurseve: emer dhe vit
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Courses>))]
        public async Task<IActionResult> Kurset()
        {
            var kurs = await _context.Courses.Select(p => new
            {
                p.Name,
                p.Year,
            }).ToListAsync();

            return Ok(kurs);
        }
    }
}