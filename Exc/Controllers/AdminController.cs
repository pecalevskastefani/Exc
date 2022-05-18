using Eshop.Domain.Domain_models;
using Eshop.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IOrderService _orderService;
        private readonly UserManager<ShopApplicationUser> userManager;
        public AdminController(IOrderService service, UserManager<ShopApplicationUser> _userManager)
        {
            _orderService = service;
            this.userManager = _userManager;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return _orderService.getAllOrders();
        }
        [HttpPost("[action]")]
        public Order GetOrderDetails(BaseEntity model)
        {
            return _orderService.getOrderDetails(model);
        }
        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model) //dali e uspesen importot ili ne
        {
            bool status = true;
            foreach(var user in model)
            {
                var userCheck = userManager.FindByEmailAsync(user.Email).Result;
                if(userCheck == null)
                {
                    var newUser = new ShopApplicationUser
                    {
                        UserName = user.Email,
                        NormalizedEmail = user.Email,
                        Email = user.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserShoppingCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync(newUser, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
    }
}
