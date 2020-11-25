using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectBun.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage = "Titlul produsului este obligatoriu")]
        public string Title { get; set;}
        
        [Required(ErrorMessage = "Descrierea produsului este obligatorie")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Pretul produsului este obligatoriu")]
        public double Price { get; set; }
        
        public int CategoryId { get; set; }
        
        //public int ReviewId { get; set; }
        
        //public int FileId { get; set; }



        //public virtual File File { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public IEnumerable<SelectListItem> Categ { get; set; }
        
    }
}