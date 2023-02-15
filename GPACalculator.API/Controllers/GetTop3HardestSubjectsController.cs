using GPACalculator.API.Db.Entities;
using GPACalculator.API.Db;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class GetTop3HardestSubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetTop3HardestSubjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-top-3-hardest-subjects")]
        public async Task<ActionResult<List<SubjectEntity>>> GetTop3Subjects()
        {

            var top3Subjects = new List<TopSubjectEntity>() { };

            var subjectCount = _context.Subjects.Count();

            for (int i = 1; i <= subjectCount; i++)
            {

                var averageSubjectScore = _context.Grades.Where(g => g.SubjectID == i)
                                                         .ToList()
                                                         .Average(g => g.Score);

                var subject = _context.Subjects.Where(s => s.Id == i).FirstOrDefault();
                var topSubject = new TopSubjectEntity() { AverageScore = averageSubjectScore, Name = subject.Name };

                top3Subjects.Add(topSubject);

                top3Subjects.Sort((s1, s2) => s1.AverageScore.CompareTo(s2.AverageScore));

                if (top3Subjects.Count > 3)
                {
                    top3Subjects.RemoveRange(3, top3Subjects.Count - 3);
                }

            }

            return Ok(top3Subjects);
        }

    }
}
