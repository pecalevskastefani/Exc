using Eshop.Domain.Domain_models;
using Eshop.Domain.DTO;
using Eshop.Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.implementation
{
    public class  ShoppingCartService : IShoppingCartService
    {
        //referenca do repository
        public readonly IUserRepository _userRepository;
        public readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public readonly IRepository<Order> _orderRepository;
        public readonly IRepository<ProductInOrder> _productInOrderRepository;
        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository
            , IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public object User { get; private set; }

        public bool deleteProductFromShoppingCart(string userId, int productId)
        {
            if (!string.IsNullOrEmpty(userId) && productId != null) {
                var loggInUser = _userRepository.Get(userId);
                var userShoppingCart = loggInUser.UserShoppingCart;
                var itemToDelete = userShoppingCart.ProductsInShoppingCarts.Where(z => z.ProductId == productId).FirstOrDefault();
                userShoppingCart.ProductsInShoppingCarts.Remove(itemToDelete);
                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {

            var user = _userRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            //листа од продукти, koristime lambda expressions za anonimni objekti
            var productList = userShoppingCart.ProductsInShoppingCarts.Select(z => new
            {
                Quantity = z.Quantity,
                ProductPrice = z.Product.Price
            });
            float totalPrice = 0;
            foreach (var item in productList)
            {
                totalPrice += item.ProductPrice * item.Quantity;
            }
            ShoppingCartDto model = new ShoppingCartDto
            {
                TotalPrice = totalPrice,
                ProductsInShoppingCart = userShoppingCart.ProductsInShoppingCarts.ToList()
            };
            return model;
        }

        public bool orderNow(string userId)
        {

            var user = _userRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;
            Order newOrder = new Order
            {
                UserId = user.Id,
                OrderBy = user
            };
            _orderRepository.Insert(newOrder);
            List<ProductInOrder> productInOrder = userShoppingCart.ProductsInShoppingCarts.Select(z => new ProductInOrder
            {
                Product = z.Product,
                ProductId = z.ProductId,
                Order = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            }).ToList();
            foreach (var item in productInOrder)
            {
                _productInOrderRepository.Insert(item);//spored tipot productInOrder si znae deka tamu treba da addne
            }
            user.UserShoppingCart.ProductsInShoppingCarts.Clear(); //empty na shoppingcart
            _userRepository.Update(user); //update na user
            return true;
        }
    }
}
