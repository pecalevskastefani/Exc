﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Domain.Domain_models
{
    public class ProductInOrder : BaseEntity
    {
        //table od many to many vrska pomegju products i order
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }

    }
}
