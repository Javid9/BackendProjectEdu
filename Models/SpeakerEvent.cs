using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class SpeakerEvent
    {
        public int Id { get; set; }
        public int UpComingEventId { get; set; }
        public UpComingEvent UpComingEvent { get; set; }
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}
