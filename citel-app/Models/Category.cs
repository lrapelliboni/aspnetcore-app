using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace citelapp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}