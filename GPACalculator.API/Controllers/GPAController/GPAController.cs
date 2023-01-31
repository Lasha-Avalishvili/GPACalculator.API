using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.API.Controllers.GPAController
{
    [ApiController]
    [Route("[controller]")]
    public class GPAController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGradeRepository _GradeRepository;

        public GPAController(IStudentRepository studentRepository, ISubjectRepository subjectRepository, IGradeRepository gradeRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _GradeRepository = gradeRepository;
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



    }
}
