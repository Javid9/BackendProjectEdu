using EducationBackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewModels
{
    public class SearchVm
    {
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }
        public string MainJson { get; set; }
    }
}
