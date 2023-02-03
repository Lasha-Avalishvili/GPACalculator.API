namespace GPACalculator.API.Db.Entities
{
    public class StudentDataEntity
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string PersonalId { get; set; } 

        public string Course { get; set; }

        public string Subject { get; set; }
        public int Score { get; set; }



    }
}
