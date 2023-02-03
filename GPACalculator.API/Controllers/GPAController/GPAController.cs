using System.Security.Cryptography.Pkcs;
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
        public async Task<ActionResult<List<GradeEntity>>> GetGrades(int studentId)
        {
            var grades = await _context.Grades.Where(u => u.StudentID == studentId).ToListAsync();
            if (!grades.Any())
            {
                return NotFound();
            }
            
            return grades;
        }

        [HttpGet("{studentId}/gpa")]
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
                if(grade.Score >=91 && grade.Score<=100)
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
                    totalCredits += 0* subject.Credit;
                }

            }
            if(total == 0 || totalCredits == 0)
            {
                return 0;
            }
            double gpa = total / totalCredits;
            return Ok(gpa);
           // return 404;

            
        }

    }
}
