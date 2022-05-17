using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Domain.Domain_models
{
    public class ShoppingCart : BaseEntity
    {  

        public string ApplicationUserId { get; set; }
        public ICollection<ProductsInShoppingCart> ProductsInShoppingCarts { get; set; } //sc moze do ovaa lista da pristapi

    }
}
