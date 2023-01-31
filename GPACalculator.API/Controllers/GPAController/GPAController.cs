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
        public GPAController(IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
                _studentRepository= studentRepository; 
            _subjectRepository = subjectRepository;
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



    }
}
