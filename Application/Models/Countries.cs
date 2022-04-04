using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Countries
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FlagPhoto { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
