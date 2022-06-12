using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exc.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController( IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            var model = _shoppingCartService.getShoppingCartInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }
        public IActionResult DeleteFromShoppingCart(int id) //vo viewto predavame id na produkt i mora da se vika id, vo root config definirano e za optional parametri mora da e id definirano
        {
            _shoppingCartService.deleteProductFromShoppingCart(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
            return RedirectToAction("Index");


        }
        public IActionResult PayOrder(string stripeEmail, string stripeToken) //mailot so koj se pravi petplatata, i samiot token za da napravime uplata
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService(); //za da jaj izvrsi uplatata

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var order = this._shoppingCartService.getShoppingCartInfo(userId); //naracka na najaaveniot korisnik

            var customer = customerService.Create(new CustomerCreateOptions //klientot 
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions //servisot za da e napravi uplatata
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "eShop application Payment",
                Currency = "usd",
                Customer = customer.Id

            }); 

            if(charge.Status == "succeeded")
            {
                var result = this.OrderNow();
                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }

            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        private Boolean OrderNow()
        {
            var model = _shoppingCartService.orderNow(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return model;

        }
    }
}
