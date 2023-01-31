using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;

namespace GPACalculator.API.Repositories
{
    public interface IGradeRepository
    {
        Task<GradeEntity> AddGradeAsync(CreateGradeRequest request);
        Task SaveChangesAsync();
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
            _db.Grades.AddAsync(grade);

            return grade;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
