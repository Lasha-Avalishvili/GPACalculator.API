using GPACalculator.API.Db.Entities;
using GPACalculator.API.Services;

namespace TestsForGPA
{
    public class Test3
    {
        private List<StudentGradeEntity> gradesList;

        [SetUp]
        public void Setup()
        {
            var studentGrade1 = new StudentGradeEntity() { Score = 88, SubjectCredits = 5 };
            var studentGrade2 = new StudentGradeEntity() { Score = 99, SubjectCredits = 10 };
            var studentGrade3 = new StudentGradeEntity() { Score = 34, SubjectCredits = 6 };
            gradesList = new List<StudentGradeEntity>() { studentGrade1, studentGrade2, studentGrade3 };
        }

        [Test]
        public void GPA_must_be_about_2_point_619()
        {
            var calculator = new CalculateGPAService();
            var result = calculator.Calculate(gradesList);
            Assert.AreEqual(Math.Round(result, 3), 2.619);
        }
    }
}