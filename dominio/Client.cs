﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Client
    {
        public Int64 Document {  get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Document.ToString()}, {Name}, {Surname}, {Email}";
        }
    }
}
