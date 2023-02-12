using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;

namespace GPACalculator.API.Repositories
{

    public interface ISubjectRepository
    {
        Task<SubjectEntity> AddSubjectAsync(CreateSubjectRequest request);
        Task SaveChangesAsync();

        bool SubjectExists(string name);
    }
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _db;
        public SubjectRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<SubjectEntity> AddSubjectAsync(CreateSubjectRequest request)
        {
            var subject = new SubjectEntity();
            subject.Name = request.Name;
            subject.Credit = request.Credit;
            _db.Subjects.AddAsync(subject);



            return subject;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool SubjectExists(string name)
        {
            return _db.Subjects.Any(x => x.Name == name);
        }

    }
}
