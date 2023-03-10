using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using GPACalculator.API.Services;
using GPACalculator.API.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class GetGPAController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GetGPAController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{studentId}/calculate-gpa")]
        public async Task<ActionResult<double>> GetGPA(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }

            var studentGrades = _context.Grades
                .Include(g => g.Subject)
                .Where(g => g.StudentID == studentId)
                .Select(g => new StudentGradeEntity
                {
                    StudentId = g.StudentID,
                    Score = g.Score,
                    SubjectCredits = g.Subject.Credit,
                })
                .ToList();

            if (!studentGrades.Any())
            {
                return NotFound();
            }

            var calculate = new CalculateGPAService();
            var gpa = calculate.Calculate(studentGrades);
            gpa = Math.Round(gpa, 3);
            return Ok(gpa);

        }

    }
}
