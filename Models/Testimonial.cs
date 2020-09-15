using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Testimonial
    {
        public int  Id { get; set; }
        public string  Image { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string  Title { get; set; }
        [Required]
        public string Position { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

    }
}
