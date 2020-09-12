using EducationBackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewModels
{
    public class HomeVm
    {
        public List<Slider> Sliders { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public List<NoticeRightInfo> NoticeRightInfos { get; set; }
        public WhyUs WhyUs { get; set; }
        public List<UpComingEvent> UpComingEvents { get; set; }
        public Testimonial Testimonial { get; set; }
        

    }
}
