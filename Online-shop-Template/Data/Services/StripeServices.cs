using Online_shop_Template.Models;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Online_shop_Template.Data.Services
{
    public class StripeServices
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IConfiguration _configuration;

        public StripeServices(IConfiguration configuration, IOptions<StripeSettings> stripeSettings)
        {
            _configuration = configuration;
            _stripeSettings = stripeSettings.Value;
        }

        

    }
}
