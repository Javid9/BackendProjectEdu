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
    public class TeacherCreateVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string VContact { get; set; }
        public string AboutMe { get; set; }
        public string Degree { get; set; }
        public string Hobby { get; set; }
        public string Faculty { get; set; }
        public string Skype { get; set; }
        public string PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Mail { get; set; }
        public int Experience { get; set; }
        public string Twitter { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<TeacherUser> TeacherUsers { get; set; }
    }
}
