using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsActivated { get; set; }
        public ICollection<CourseUser> CourseUsers{ get; set; }
        public ICollection<TeacherUser> TeacherUsers { get; set; }


    }
}
