using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Telphone { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string Country { get; set; }

        public string Photo { get; set; }

        //many to many
        public List<Friendship> Friendship_ { get; set; }

        //one to many
        public List<Countries> Countries_ { get; set; }
    }
}
