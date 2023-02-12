using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GPACalculator.API.Validations
{
    public class AddSubjectValidator
    {
        private readonly ISubjectRepository _repository;

        public AddSubjectValidator(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public void Validate(CreateSubjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Name.IsNullOrEmpty())
            {
                throw new ArgumentException("Name field cannot be empty");
            }
            if (request.Credit == null)
            {
                throw new ArgumentException("Credit field cannot be empty");
            }
            if(request.Credit < 1)
            {
                throw new ArgumentException("Credit cant be less than 1");
            }
            
            var subjectExists = _repository.SubjectExists(request.Name);
            if (subjectExists)
            {
                throw new Exception($"Subject with the name: {request.Name} already exists.");
            }

        }
    }
}
