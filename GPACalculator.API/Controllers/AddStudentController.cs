using System.ComponentModel.DataAnnotations;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using GPACalculator.API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]

    public class AddStudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly AddStudentValidator _validator;
        public AddStudentController(IStudentRepository studentRepository, AddStudentValidator validator)
        {
            _studentRepository= studentRepository;
            _validator= validator;
        }

        [HttpPost("register-student")]
        public async Task<ActionResult<StudentEntity>> AddStudent([FromBody] CreateStudentRequest request)
        {
            _validator.Validate(request);
            var student = await _studentRepository.AddStudentAsync(request);
            await _studentRepository.SaveChangesAsync();

            return Ok(student);

        } 

    }
}
