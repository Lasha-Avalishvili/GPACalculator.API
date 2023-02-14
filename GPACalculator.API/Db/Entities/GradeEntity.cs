namespace GPACalculator.API.Db.Entities
{
    public class GradeEntity
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int SubjectID { get; set; }
        public int StudentID { get; set; }

        public SubjectEntity Subject { get; set; }

    }
}
  