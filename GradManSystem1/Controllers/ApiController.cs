//using GradManSystem1.Data;
//using GradManSystem1.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;



//namespace GradManSystem1.Controllers
//{

//    [ApiController]
//    [Route("api/Api")]
//    public class ApiController : ControllerBase
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly ApplicationDbContext _context;
//        public ApiController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
//        {
//            _context = context;
//            _webHostEnvironment = webHostEnvironment;
//        }

        //afisho studentet sipas vitit te lindjes
        //[HttpGet("/Birthday")]
        
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        //public IActionResult Birthday()
        //{
        //    var result = from Student in _context.Student
        //                 group Student by new { Student.Birthday.Year } into rezultat
        //                 select new
        //                 {
        //                     Viti = new DateTime(rezultat.Key.Year, 1, 1).ToString("yyyy"),
        //                     Count = rezultat.Count()
        //                 };
        //    return Ok(result);
        //}
        //afisho departamentet sipas nr te kurseve
        //[HttpGet("/NumberOfCourses")]

        //[ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        //public IActionResult NumberOfCourses()
        //{
        //    var result = from Department in _context.Department
        //                 group Department by new { Department.NumberOfCourses } into rezultat
        //                 select new
        //                 {
        //                     Dep = rezultat.Key,
        //                     Count = rezultat.Count()
        //                 };
        //    return Ok(result);
        //}
        //Merr nr perdoruesve
        //[HttpGet("/NumriPerdoruesve")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        //public IActionResult NumriPerdoruesve()
        //{
        //    return Ok(_context.Student.Count());
        //}
        ////merr nr kurseve
        //[HttpGet("/NumriKurseve")]
        //public IActionResult NumriKurseve()
        //{
        //    return Ok(_context.Courses.Count());
        //}

        //tabele kurseve: emer dhe vit
        //[HttpGet("/Kurset")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Courses>))]
        //public async Task<IActionResult> Kurset()
        //{
        //    var kurs = await _context.Courses.Select(p => new
        //    {
        //        p.Name,
        //        p.Year,
        //    }).ToListAsync();

        //    return Ok(kurs);
        //}
//    }
//}