﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Friendship> Friendship { get; set; }
    }
}
