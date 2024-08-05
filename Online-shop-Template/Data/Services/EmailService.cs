using Azure.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using Online_shop_Template.Models;



namespace Online_shop_Template.Data.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration, IOptions<StripeSettings> stripeSettings) 
        {
            _configuration = configuration;

        }

        public async Task SendRegistrationEmailAsync(string fromName, string toEmail)
        {
            //For retrieving Appsettings.
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Environment.CurrentDirectory)
           .AddJsonFile("appsettings.json")
           .Build();


            // Retrieve Azure Key Vault settings from appsettings.json
            string keyVaultUrl = _configuration["KeyVault:KeyVaultURL"];
            string clientId = _configuration["KeyVault:ClientId"];
            string clientSecret = _configuration["KeyVault:ClientSecret"];
            string tenantId = _configuration["KeyVault:DirectoryID"];

            // Create a new SecretClient using ClientSecretCredential
            var aclient = new SecretClient(new Uri(keyVaultUrl), new ClientSecretCredential(tenantId, clientId, clientSecret));
            KeyVaultSecret gPass = aclient.GetSecret("GmailPassword");
            KeyVaultSecret gUser = aclient.GetSecret("GmailUserName");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(gUser.Value, gUser.Value));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = "Registration at Shanks Warrior Appeal.";
            emailMessage.Body = new TextPart("plain") { Text = "Hello " + fromName + ". Welcome to Shanks Warrior Appeal. Thanks for registering." };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(gUser.Value, gPass.Value);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendDeleteEmailAsync(string fromName, string toEmail)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Environment.CurrentDirectory)
           .AddJsonFile("appsettings.json")
           .Build();


            // Retrieve Azure Key Vault settings from appsettings.json
            string keyVaultUrl = _configuration["KeyVault:KeyVaultURL"];
            string clientId = _configuration["KeyVault:ClientId"];
            string clientSecret = _configuration["KeyVault:ClientSecret"];
            string tenantId = _configuration["KeyVault:DirectoryID"];

            // Create a new SecretClient using ClientSecretCredential
            var aclient = new SecretClient(new Uri(keyVaultUrl), new ClientSecretCredential(tenantId, clientId, clientSecret));
            KeyVaultSecret gPass = aclient.GetSecret("GmailPassword");
            KeyVaultSecret gUser = aclient.GetSecret("GmailUserName");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(gUser.Value, gUser.Value));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = "Account deleted at Shanks Warrior Appeal.";
            emailMessage.Body = new TextPart("plain") { Text = "Hello " + fromName + ". We are sorry to see you leave us. We hope you enjoyed your time with us and are welcome at anytime. Thank you." };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(gUser.Value, gPass.Value);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
