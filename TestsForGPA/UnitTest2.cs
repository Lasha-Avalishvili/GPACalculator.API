using GPACalculator.API.Db.Entities;
using GPACalculator.API.Services;

namespace TestsForGPA
{
    public class Test2
    {
        private List<StudentGradeEntity> gradesList;

        [SetUp]
        public void Setup()
        {
            var studentGrade1 = new StudentGradeEntity() { Score = 96, SubjectCredits = 6 };
            var studentGrade2 = new StudentGradeEntity() { Score = 83, SubjectCredits = 4 };
            var studentGrade3 = new StudentGradeEntity() { Score = 77, SubjectCredits = 5 };
            gradesList = new List<StudentGradeEntity>() { studentGrade1, studentGrade2, studentGrade3 };
        }

        [Test]
        public void GPA_must_be_about_3_or_little_more()
        {
            var calculator = new CalculateGPAService();
            var result = calculator.Calculate(gradesList);
            Assert.AreEqual(Math.Round(result, 3), 3.067);
        }
    }
}
