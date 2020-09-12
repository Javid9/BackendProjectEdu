using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Teacher: BaseEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string VContact { get; set; }
        public string Twitter { get; set; }
    }
}
