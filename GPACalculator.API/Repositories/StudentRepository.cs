﻿using GPACalculator.API.Db;
using GPACalculator.API.Db.Entities;
using GPACalculator.API.Models.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GPACalculator.API.Repositories
{
    public interface IStudentRepository
    {
        Task<StudentEntity> AddStudentAsync(CreateStudentRequest request);
        Task SaveChangesAsync();    
        
        bool StudentExists(string personalId);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;
        public StudentRepository(AppDbContext db)
        {
            _db= db;
        }

        public async Task<StudentEntity> AddStudentAsync(CreateStudentRequest request)
        {
            var student = new StudentEntity();
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Course = request.Course;
            student.PersonalID= request.PersonalID;
            await _db.Students.AddAsync(student);  

            return student;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool StudentExists(string personalId)
        {
            return _db.Students.Any(s => s.PersonalID == personalId);
        }


    }
}
