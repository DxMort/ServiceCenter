using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace ServiceCentre.Models
{
    public class Parts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string ImagePath { get; set; }
        public string CellNumber { get; set; }
        public string SupportedModels { get; set; }
    }
}