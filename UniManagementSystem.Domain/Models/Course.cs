﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        public int MaxDegree { get; set; } = 100;
        public int MinDegree { get; set; } = 50;
        public bool IsActive { get; set; } = true;

        [MaxLength(50)]
        public string CourseCode { get; set; }


        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }


        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        //public ICollection<Exam> Exams { get; set; } = new List<Exam>();
     //   public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
