using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class UpComingEvent
    {
        public int Id { get; set; }
        public DateTime Month { get; set; }
        public DateTime Day { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<SpeakerEvent> SpeakerEvents { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
