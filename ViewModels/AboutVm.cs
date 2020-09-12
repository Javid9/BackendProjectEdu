using EducationBackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewModels
{
    public class AboutVm
    {
        public WelcomeAbout WelcomeAbout { get; set; }
        public Testimonial Testimonial { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public VideoTour VideoTour { get; set; }

    }
}
