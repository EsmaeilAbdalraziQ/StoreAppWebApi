using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public decimal? ProductPrice { get; set; }
        public DateTime LastUpdated { get; set; }
        
        //[ForeignKey("Photo")]
        //public int? PhotoId { get; set; }
        //public Photo Photo { get; set; }
    }
}
