using GPACalculator.API.Db.Entities;
using GPACalculator.API.Db;
using Microsoft.AspNetCore.Mvc;
using GPACalculator.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class GetTop10GPAStudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetTop10GPAStudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-top-10-gpa-students")]
        public async Task<ActionResult<List<StudentGPAEntity>>> GetTop10Students()
        {
           
            var topGPAStudents = new List<StudentGPAEntity>() { };

            var studentscount = _context.Students.Count();
            for (int i = 1; i <= studentscount; i++)
            {

                var studentGrades = await _context.Grades
                    .Where(g => g.StudentID == i)
                    .Include(g => g.Subject)
                    .Select(g => new StudentGradeEntity
                    {
                        StudentId = g.StudentID,
                        Score = g.Score,
                        SubjectCredits = g.Subject.Credit,
                    })
                    .ToListAsync();

                if (!studentGrades.Any())
                {
                    continue;
                }

                var calculate = new CalculateGPAService();
                var gpa = calculate.Calculate(studentGrades);

                var student = _context.Students.Where(s => s.Id== i).FirstOrDefault();
                var studentgpa = new StudentGPAEntity() { GPA = gpa, FirstName = student.FirstName, LastName = student.LastName };
                topGPAStudents.Add(studentgpa);
            }

            topGPAStudents.Sort((s2, s1) => s1.GPA.CompareTo(s2.GPA)); // Sort by GPA in descending order
           
            if (topGPAStudents.Count > 10)
            {
                topGPAStudents.RemoveRange(10, topGPAStudents.Count - 10); // Remove elements beyond index 9
            }
            return Ok(topGPAStudents);
        }

    }
}
