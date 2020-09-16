using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class CourseUser
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser  AppUser { get; set; }
        public int CourseId { get; set; }
        public Course  Course { get; set; }
    }
}
