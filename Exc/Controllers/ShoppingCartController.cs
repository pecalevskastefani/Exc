using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
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

        public IActionResult OrderNow()
        {
            var model = _shoppingCartService.orderNow(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index"); //moze i nameof(Index)

        }
    }
}
