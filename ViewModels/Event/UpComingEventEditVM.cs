using EducationBackendFinal.Mapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewModels
{
    public class UpComingEventEditVM
    {
        public int Id { get; set; }
        [Required]
        public DateTime Month { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
       
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public IFormFile Photo { get; set; }
        public List<int> SpeakersId { get; set; }
        public string Image { get; set; }
    }
}
