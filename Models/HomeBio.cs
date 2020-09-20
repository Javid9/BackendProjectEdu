using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Models
{
    public class HomeBio
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Logo { get; set; }
        public string Facebook { get; set; }
        public string Vcontact { get; set; }
        public string Twitter { get; set; }
        public string Pinterest { get; set; }
        [NotMapped]

        public IFormFile Photo { get; set; }
    }
}
