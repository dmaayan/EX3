using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EX3.Models
{
    /// <summary>
    /// User entity for the data base
    /// </summary>
    public class User
    {
        /// <summary>
        /// userName property of user. primary key
        /// </summary>
        [Key]
        public string userName { get; set; }

        /// <summary>
        /// password property of user
        /// </summary>
        [Required]
        public string password { get; set; }

        /// <summary>
        /// email property of user
        /// </summary>
        [Required]
        public string email { get; set; }

        /// <summary>
        /// Statistics property of user. Foreign Key
        /// </summary>
        [ForeignKey("userName")]
        public Statistics StatisticsUserName { get; set; }
    }
}