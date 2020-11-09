using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProiectBun.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public string Title { get; set;}
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public virtual Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; }
        
    }
}