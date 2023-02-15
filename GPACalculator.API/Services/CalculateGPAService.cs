using GPACalculator.API.Db.Entities;

namespace GPACalculator.API.Services
{
    public class CalculateGPAService
    {
        public double Calculate(List<StudentGradeEntity> studentGrades)
        {
            double AllGP = 0;
            double AllCredits = 0;

            foreach (var studentGrade in studentGrades)
            {
                AllGP += GetGP(studentGrade.Score)* studentGrade.SubjectCredits;
                AllCredits += studentGrade.SubjectCredits;
            }

            double GPA = AllGP/ AllCredits;

            return GPA;
        }


        public double GetGP(int score)
        {
            double GP;
            if (score <= 100 && score > 90)
            {
                GP = 4;   
            }
            else if (score <= 90 && score > 80)
            {
                GP = 3;
            }
            else if (score <= 80 && score > 70)
            {
                GP = 2;
            }
            else if (score <= 70 && score > 60)
            {
                GP = 1;
            }
            else if (score <= 60 && score > 50)
            {
                GP = 0.5;
            }
            else
            {
                GP = 0;
            }
            return GP;
        }


    }
}
