using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public ICollection<SpeakerEvent> SpeakerEvents { get; set; }
    }
}
