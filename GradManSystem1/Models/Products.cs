﻿
using System.ComponentModel.DataAnnotations;

namespace GradManSystem1.Models
{
    public class Products 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
    }
}
