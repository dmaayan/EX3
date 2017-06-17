using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    public class User
    {
        [Key]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        [ForeignKey("userName")]
        public Statistics StatisticsUserName { get; set; }
    }
}