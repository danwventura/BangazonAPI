using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Providers.Models
{
    public class Chore
    {
        public enum Status
        {
            ToDo,
            InProgress,
            Complete
        }

        [Key]
        public int ChoreID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Status status { get; set; }
        [Required]
        public DateTime CompletedOn { get; set; }
        
    }
}