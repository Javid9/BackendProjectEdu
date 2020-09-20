using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //public ICollection<Course> Courses { get; set; }
        public virtual ICollection<UpComingEvent> UpComingEvents { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }

    }
}
