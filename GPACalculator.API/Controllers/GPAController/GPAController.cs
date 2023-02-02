using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.API.Controllers.GPAController
{
    [ApiController]
    [Route("[controller]")]
    public class GPAController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGradeRepository _GradeRepository;
        private readonly AppDbContext _context;

        public GPAController(IStudentRepository studentRepository, 
                             ISubjectRepository subjectRepository, 
                             IGradeRepository gradeRepository,
                             AppDbContext context)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _GradeRepository = gradeRepository;
            _context = context;
        }
        [HttpPost("Register Student")] 
        public async Task<ActionResult<StudentEntity>> AddStudent([FromBody] CreateStudentRequest request)
        {
            var student = await _studentRepository.AddStudentAsync(request);
            await _studentRepository.SaveChangesAsync();
             
            return Ok(student);
        }
        [HttpPost("Register Subject")]
        public async Task<ActionResult<SubjectEntity>> AddSubject([FromBody] CreateSubjectRequest request)
        {
            var subject = await _subjectRepository.AddSubjectAsync(request);
            await _subjectRepository.SaveChangesAsync();

            return Ok(subject);
        }

        [HttpPost("Add Grade")]
        public async Task<ActionResult<GradeEntity>> AddGrade([FromBody] CreateGradeRequest request)
        {
            var grade = await _GradeRepository.AddGradeAsync(request);
            await _GradeRepository.SaveChangesAsync();

            return Ok(grade);
        }

        [HttpGet("{studentId}/grades")]

        //public async Task<ActionResult<GradeEntity>> GetUser(int studentId)
        //{
        //    var grades = await _context.Grades.Where(u => u.StudentID == studentId).FirstOrDefaultAsync();
        //    if (grades == null)
        //    {
        //        return NotFound();
        //    }

        //    return grades;
        //}
        public async Task<ActionResult<List<GradeEntity>>> GetGrades(int studentId)
        {
            var grades = await _context.Grades.Where(u => u.StudentID == studentId).ToListAsync();
            if (!grades.Any())
            {
                return NotFound();
            }

            // we got grade/s here

            // but we need to get student's name too
            var student = _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student==null)
            {
                return NotFound();
            }
            // we dont know subject name yet
            //var subject = _context.Subjects.Where(j => j.Id==grades.) we'll need a list probably



            return grades;    // + student;
        }

    }
}
