using GPACalculator.API.Models.Requests;
using GPACalculator.API.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GPACalculator.API.Validations
{
    public class AddStudentValidator
    {
        private readonly IStudentRepository _repository;

        public AddStudentValidator(IStudentRepository repository)
        {
            _repository = repository;
        }

        public void Validate(CreateStudentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if(request.FirstName.IsNullOrEmpty()) 
            {
                throw new ArgumentException("Name field cannot be empty");
            }
            if (request.LastName.IsNullOrEmpty())
            {
                throw new ArgumentException("LastName field cannot be empty");
            }
            if(request.PersonalID.IsNullOrEmpty())
            {
                throw new ArgumentException("ID field cant be empty");
            }
            if(request.PersonalID.Count()<5)
            {
                throw new ArgumentException("ID cant be less than 5 characters");
            }
            if (request.Course.IsNullOrEmpty()) 
            {
                throw new ArgumentException("Course cant be empty");
            }

            var studentExists = _repository.StudentExists(request.PersonalID);
            if (studentExists)
            {
                throw new Exception($"Student with Personal ID number: {request.PersonalID} already exists."); 
            }

        }

    }
}
