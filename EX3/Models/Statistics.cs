using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    public class Statistics
    {
        [Key]
        public string UserName { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }
    }
}