using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCentre.Models
{
    [Table("Logs")]
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}