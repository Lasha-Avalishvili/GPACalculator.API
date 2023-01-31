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

        public GPAController(IStudentRepository studentRepository)
        {
                _studentRepository= studentRepository;  
        }
        [HttpPost] 
        public async Task<ActionResult<StudentEntity>> AddStudent([FromBody] CreateStudentRequest request)
        {
            var student = await _studentRepository.AddStudentAsync(request);
            await _studentRepository.SaveChangesAsync();
             
            return Ok(student);
        }

    }
}
