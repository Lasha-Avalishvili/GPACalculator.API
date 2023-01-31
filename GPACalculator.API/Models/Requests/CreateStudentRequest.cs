using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Build.Framework;

namespace GPACalculator.API.Models.Requests
{
    public class CreateStudentRequest
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalID { get; set; }

        public string Course { get; set; }
    }
}
