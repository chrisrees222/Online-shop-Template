# Online-shop-Template

An ecommerce shopping template app complete with Email services and stripe payments based on coffee products built from .Net6 core.

The app settings consist of secret keys where email is from Azure key vault.

simply input your key settings including connection string for database.

The stripe account is not through azure key vault as not configured on Azure yet for test mode.
Stripe is used for code simplicity as it automattically incorporates Google pay, Apple pay, link and many others if configured including Paypal.
These can be inserted straight into appSettings.

If no account. When purchasing, select the "Complete Order" Button rather than checkout, this will carry out the actions of a dummy payment and show orders.

# The required NuGet packages are:-
Azure.Extensions.AspNetCore.Configuration.Secrets
Azure.Identity
Azure.Security.KeyVault.Secrets
MailKit
Microsoft.Aspnetcore.Identity.EntityFrameworkcore
Microsoft.Data.SqlClient
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.sqlserver
Microsoft.EntityFrameworkCore.tools
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Azurekeyvault
Microsoft.Extensions.Configuration.Binder
Microsoft.Extensions.Configuration.Json
Microsoft.SqlServer.SqlManagementObjects
Mimekit
Stripe.net

# Once entered, use data migrations in package manager.
Add-Migration "name"
Update-Database

# for local stripe testing of the webhook.
Install the stripe cli, for quick testing, download and extract to desktop.
in command prompt, type cd Desktop.
followed by "Stripe login" which will then ask you to press enter and display a confirm web page with a secret number.
in command prompt, a "whes...." will display, ensure this is in appsettings under endpoint.
in command prompt, enter = stripe listen --forward-to https://localhost:7027/webhook where the 7027 will need to be placed with what ever local port number your using.
to find this, run app and look at the web endpoint number, then close down to reset.
once entered and forward local is running. Run the app leaving command prompt running.
select product and checkout using checkout button.
when you select pay. look at command prompt for 200 green response.

For stripe payment, when the user buys a product, it is directed to a create checkout session hosted by Stripe.
Once checkout is complete, the checkout Id is stored in users Model.
When the webhook is succesful with response code 200, it looks for the user with the stored Id as it matches, then alters the paid status
to confirm payment for that particular user. 
#This will show for both user and admin.

# The database is seeded for instant use with an admin and a user role.
Using Identity for users and admin. When a user registers, an email is sent, along with email sent when removing account.

admin user:-
email = admin@coffee.com
password = coffee@1234

user:-
email = user@coffee.com
password = coffee1234

When admin is logged in, it has the additional benefits of altering products and users. 
Additionally, when you admin user looks at his orders, it reveals all orders from users including a status column. This is
linked to Stripe webhook which confirms payments that are made.

In orders, it will highlight green for both user and admin to assist with user experience.

