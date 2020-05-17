using System;
using System.ComponentModel.DataAnnotations;

namespace citelapp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, 999.99)]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}