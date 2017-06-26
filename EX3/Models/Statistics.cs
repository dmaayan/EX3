using System.ComponentModel.DataAnnotations;

namespace EX3.Models
{
    /// <summary>
    /// Statistics entity for the data base
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// UserName property of Statistics. primary key
        /// </summary>
        [Key]
        public string UserName { get; set; }

        /// <summary>
        /// Wins property of Statistics.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Losses property of Statistics.
        /// </summary>
        public int Losses { get; set; }
    }
}