using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCentre.Models
{
    public class Requests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? CurrentMasterId { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Description { get; set; }

        public virtual Clients Client { get; set; }
        public virtual Users CurrentMaster { get; set; }
    }
}