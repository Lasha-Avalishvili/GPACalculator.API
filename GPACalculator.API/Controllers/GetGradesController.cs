using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using GPACalculator.API.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class GetGradesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetGradesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}/get-grades")]
        public async Task<ActionResult<List<GradeEntity>>> GetGrades(int studentId)
        {

            var grades = await _context.Grades.Where(g => g.StudentID== studentId).ToListAsync();
            if(!grades.Any()) 
            {
                throw new Exception("No grades by this ID");
            }
            
            return Ok(grades);
        }

    }
}
