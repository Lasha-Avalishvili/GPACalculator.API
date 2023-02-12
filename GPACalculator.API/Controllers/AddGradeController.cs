using System.Drawing.Printing;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using GPACalculator.API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.API.Controllers
{
    [ApiController]
    [Route("API")]
    public class AddGradeController : ControllerBase
    {
        private readonly IGradeRepository _GradeRepository;
        private readonly AddGradeValidator _GradeValidator;
        public AddGradeController(IGradeRepository gradeRepository, AddGradeValidator gradeValidator)
        {
            _GradeRepository = gradeRepository;
            _GradeValidator = gradeValidator;
        }

        [HttpPost("add-grade")]
        public async Task<ActionResult<GradeEntity>> AddGrade([FromBody] CreateGradeRequest request)
        {
            _GradeValidator.Validate(request);
            var grade = await _GradeRepository.AddGradeAsync(request);
            await _GradeRepository.SaveChangesAsync();

            return Ok(grade);
        }

    }
}
