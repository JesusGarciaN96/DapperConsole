﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperConsole.Models
{
    public class PaisDto
    {
        public int Id { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}