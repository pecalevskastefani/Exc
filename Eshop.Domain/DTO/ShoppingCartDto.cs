using Eshop.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Domain.DTO
{
    public class ShoppingCartDto
    {
        //ni trebaat produktite vo shopping cartot
        public List<ProductsInShoppingCart> ProductsInShoppingCart { get; set; }
        public float TotalPrice { get; set; }
    }
}
