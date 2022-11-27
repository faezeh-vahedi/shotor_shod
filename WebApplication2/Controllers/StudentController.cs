using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<ActionResult<Student>> GetStudents()
        {

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet]
        [Route("GetStudent")]
        public async Task<ActionResult<Student>> GetStudent(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student == null)
                return BadRequest("No Student found!");

            return Ok(student);
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<ActionResult<Student>> AddStudent(Student request)
        {
            _context.Students.Add(request);

            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<ActionResult<Student>> UpdateStudent(Student request)
        {
            var dbStudent = await _context.Students.FindAsync(request);
            if (dbStudent == null)
                return BadRequest("No Student found!");

            dbStudent.Name = request.Name;
            dbStudent.Age = request.Age;
            dbStudent.City = request.City;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<ActionResult<Student>> DeleteStudent(int Id)
        {
            var dbStudent = await _context.Students.FindAsync(Id);
            if (dbStudent == null)
                return BadRequest("No Student found!");

            _context.Students.Remove(dbStudent);

            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
    }
}
