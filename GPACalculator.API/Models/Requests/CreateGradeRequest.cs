namespace GPACalculator.API.Models.Requests
{
    public class CreateGradeRequest
    {
        public int StudentID { get; set; }
         public int SubjectId { get; set;}
        public int Score { get; set;}
    }
}
