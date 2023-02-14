﻿using GPACalculator.API.Db.Entities;

namespace GPACalculator.API.Services
{
    public class CalculateGPAService
    {
        public double Calculate(List<StudentGradeEntity> grades)
        {
            //we have  Score and Credit we need to get - GPA

            // step1 getGP 

            // step2 sum all GP * credit

            // step3 sum all credits

            // step 4 (all GP * credits)/ (all credits)
            double AllGP = 0;
            double AllCredits = 0;

            foreach (var grade in grades)
            {
                AllGP += GetGP(grade.Score)*grade.SubjectCredits;
                AllCredits += grade.SubjectCredits;
            }

            double GPA = AllGP/ AllCredits;

            return GPA;
        }


        public double GetGP(int score)
        {
            double GP = 0;
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
