using Eshop.Domain.Domain_models;
using Eshop.Domain.DTO;
using Eshop.Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.implementation
{
    public class ProductService : IProductService

    {
        //referenca do repository
        public readonly IRepository<Product> _productRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<ProductsInShoppingCart> _productsInShoppingCart;
        public ProductService(IRepository<Product> productRepository,IUserRepository userRepository, 
            IRepository<ProductsInShoppingCart> productsInShoppingCart)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productsInShoppingCart = productsInShoppingCart;
        }
    public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(int id)
        {
            return _productRepository.Get(id);
        }

        public void CreateNewProduct(Product p)
        {
            this._productRepository.Insert(p);
        }

        public void UpdateExistingProduct(Product p)
        {
            _productRepository.Update(p);
        }

        public ShoppingCartDto GetShoppingCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.Get(id);
            this._productRepository.Delete(product);
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserShoppingCart;

            if ( userShoppingCard != null)
            {
                var product = this.GetDetailsForProduct(item.ProductId);

                if (product != null)
                {
                    ProductsInShoppingCart itemToAdd = new ProductsInShoppingCart
                    {
                      
                       Product = product,
                        ProductId = product.Id,
                        ShoppingCart = userShoppingCard,
                        CartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._productsInShoppingCart.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }


       
    }
}
