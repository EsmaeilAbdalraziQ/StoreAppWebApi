using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoPath { get; set; }
    }
}
