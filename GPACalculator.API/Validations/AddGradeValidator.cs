using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;

namespace GPACalculator.API.Validations
{
    public class AddGradeValidator
    {   
        private readonly IGradeRepository _repository;

        public AddGradeValidator(IGradeRepository repository)
        {
            _repository = repository;
        }

        public void Validate(CreateGradeRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.StudentID <= 0)
            {
                throw new ArgumentException(nameof(request.StudentID));
            }
            if (request.SubjectId <= 0)
            {
                throw new ArgumentException(nameof(request.SubjectId));
            }
             
            var gradeExists = _repository.Exists(request.StudentID, request.SubjectId);
            if (gradeExists)
            {
                throw new Exception(
                    $"Grade for student {request.StudentID} for subject {request.SubjectId} already exists.");
            }

        }
    }
}
