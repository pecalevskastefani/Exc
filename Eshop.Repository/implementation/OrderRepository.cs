using EEshop.Repository;
using Eshop.Domain.Domain_models;
using Eshop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Repository.implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;

            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities.Include(z => z.OrderBy).Include(z => z.Products).Include("Products.Product").ToList();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return entities.Include(z => z.OrderBy).Include(z => z.Products).Include("Products.Product")
                .SingleOrDefault(z => z.Id == model.Id);
        }
    }
}
