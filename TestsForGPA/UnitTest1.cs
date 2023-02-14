using GPACalculator.API.Db.Entities;
using GPACalculator.API.Services;

namespace TestsForGPA
{
    public class Tests
    {
        private List<StudentGradeEntity> gradesList;

        [SetUp]
        public void Setup()
        {
            var studentGrade1 = new StudentGradeEntity() { Score = 100, SubjectCredits = 2 };
            var studentGrade2 = new StudentGradeEntity() { Score = 95, SubjectCredits = 5 };
            var studentGrade3 = new StudentGradeEntity() { Score = 91, SubjectCredits = 7 };
            gradesList = new List<StudentGradeEntity>() { studentGrade1, studentGrade2, studentGrade3 }; 
        }

        [Test]
        public void All_Scores_Are_Between_91_and_100_so_GPA_must_be_4()
        {
            var calculator = new CalculateGPAService();
            var result = calculator.Calculate(gradesList);
            Assert.AreEqual(result, 4);
        }
    }
}

