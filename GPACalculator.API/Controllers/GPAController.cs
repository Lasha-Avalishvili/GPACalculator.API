using System.Security.Cryptography.Pkcs;
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
    public class GPAController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GPAController(AppDbContext context)
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

            var grades = _context.Grades.Where(g => g.StudentID == studentId).ToList();
            if (!grades.Any())
            {
                return NotFound();
            }

            double totalCredits = 0;
            double total = 0;
            foreach (var grade in grades)
            {
                var subject = await _context.Subjects.FindAsync(grade.SubjectID);
                if (subject == null)
                {
                    return NotFound();
                }
                if (grade.Score >= 91 && grade.Score <= 100)
                {
                    total += 4 * subject.Credit;
                    totalCredits += subject.Credit;
                }
                else if (grade.Score >= 81 && grade.Score <= 90)
                {
                    total += 3 * subject.Credit;
                    totalCredits += subject.Credit;
                }
                else if (grade.Score >= 71 && grade.Score <= 80)
                {
                    total += 2 * subject.Credit;
                    totalCredits += subject.Credit;
                }
                else if (grade.Score >= 61 && grade.Score <= 70)
                {
                    total += 1 * subject.Credit;
                    totalCredits += subject.Credit;
                }
                else if (grade.Score >= 51 && grade.Score <= 60)
                {
                    total += 0.5 * subject.Credit;
                    totalCredits += subject.Credit;
                }
                else if (grade.Score <= 50)
                {
                    total += 0 * subject.Credit;
                    totalCredits += 0 * subject.Credit;
                }

            }

            if (total == 0 || totalCredits == 0)
            {
                return 0;
            }
            double gpa = total / totalCredits;
            return Ok(gpa);

        }

    }
}
