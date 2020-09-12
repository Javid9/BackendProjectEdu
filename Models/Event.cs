using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Month { get; set; }
        public DateTime Day { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Image { get; set; }
    }
}
