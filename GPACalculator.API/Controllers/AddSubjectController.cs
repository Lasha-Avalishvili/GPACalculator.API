using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using GPACalculator.API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class AddSubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly AddSubjectValidator _validator;
        public AddSubjectController(ISubjectRepository subjectRepository, AddSubjectValidator validator)
        {
            _subjectRepository = subjectRepository;
            _validator = validator;
        }

        [HttpPost("register-subject")]
        public async Task<ActionResult<SubjectEntity>> AddSubjectt([FromBody] CreateSubjectRequest request)
        {
            _validator.Validate(request);
            var subject = await _subjectRepository.AddSubjectAsync(request);
            await _subjectRepository.SaveChangesAsync();

            return Ok(subject);

        }

    }
}
