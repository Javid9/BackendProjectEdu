using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewModels
{
    public class CourseEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public string AboutCourse { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public int Duration { get; set; }
        [Required]
        public int CourseFee { get; set; }
        public string ClassDuration { get; set; }
        [Required]
        public string SkilLevel { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int StudentsCount { get; set; }
        [Required]
        public string Assesments { get; set; }

        public ICollection<CourseUser> CourseUsers { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
