using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public int Language { get; set; }
        [Required]
        public int TeamLeader { get; set; }
        [Required]
        public int Development { get; set; }
        [Required]
        public int Design { get; set; }
        [Required]
        public int Innovation { get; set; }
        [Required]
        public int Communication { get; set; }
        [Required]
        public int TeacherId { get; set; }
       
        public Teacher Teacher { get; set; }
    }
}
