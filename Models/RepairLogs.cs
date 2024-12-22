using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCentre.Models
{
    public class RepairLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string WorkDescription { get; set; }
        public DateTime WorkDate { get; set; }
        public int? PartId { get; set; }
        public decimal AddedCost { get; set; }


        public virtual Requests RepairRequest { get; set; }
        public virtual Users User { get; set; }
        [ForeignKey("PartId")]
        public virtual Parts Part { get; set; }
    }
}