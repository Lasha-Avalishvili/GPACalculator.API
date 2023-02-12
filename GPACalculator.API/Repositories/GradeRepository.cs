using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;

namespace GPACalculator.API.Repositories
{
    public interface IGradeRepository
    {
        Task<GradeEntity> AddGradeAsync(CreateGradeRequest request);
        Task SaveChangesAsync();

        bool Exists(int studentId, int subjectId);
    }
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _db;
        public GradeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<GradeEntity> AddGradeAsync(CreateGradeRequest request)
        {
            var grade = new GradeEntity();
            grade.SubjectID = request.SubjectId;
            grade.StudentID = request.StudentID; 
            grade.Score = request.Score;
            await _db.Grades.AddAsync(grade);

            return grade;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool Exists(int studentId, int subjectId)
        {
            return _db.Grades.Any(g => g.StudentID == studentId && g.SubjectID == subjectId);
        }
    }
}
