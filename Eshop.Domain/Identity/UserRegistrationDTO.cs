using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Domain.Identity
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
