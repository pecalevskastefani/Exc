using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Domain.Domain_models
{
    public class Product : BaseEntity
    {

 
        [Required]
        [Display(Name = "Име на продукт")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int Rating { get; set; }
        public ICollection<ProductsInShoppingCart> ProductsInShoppingCarts {get;set;} //product moze do ovaa lista da pristapi
    }
}
