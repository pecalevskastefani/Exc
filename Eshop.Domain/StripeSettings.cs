﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Domain
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}