namespace GPACalculator.API.Db.Entities
{
    public class StudentEntity
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalID { get; set;}

        public string Course { get; set; }  

     
    }
}
