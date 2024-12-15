using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models.Enums;
using CoffeeCrazy.Repos;
using System.Net;
using System.Net.Mail;

namespace CoffeeCrazy.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepo _tokenGeneratorRepo;
        private readonly IPasswordRepo _passwordRepo;

        public EmailService(IConfiguration configuration, ITokenRepo tokenGeneratorRepo, IPasswordRepo passwordRepo)
        {
            _configuration = configuration;
            _tokenGeneratorRepo = tokenGeneratorRepo;
            _passwordRepo = passwordRepo;
        }

        /// <summary> 
        /// Sends an email with a specified subject and body to a recipient. 
        /// </summary>
        /// <param name="emailToResive">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email, formatted as HTML.</param>
        /// <returns>
        /// True if the email is sent successfully; false otherwise.
        /// </returns>
        /// <exception cref="SmtpException">Thrown if there are issues with the SMTP client configuration.</exception>
        /// <exception cref="Exception">Thrown for any other errors encountered during email sending.</exception>
        public async Task<bool> SendEmailAsync(string emailToResive, string subject, string body)
        {
            try
            {
                // Retrieve SMTP settings from appsettings
                string smtpSetting = SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[$"{smtpSetting}:{SmtpSettings.Host}"];
                int.TryParse(_configuration[$"{smtpSetting}:{SmtpSettings.Port}"], out int port);
                string username = _configuration[$"{smtpSetting}:{SmtpSettings.Username}"];
                string password = _configuration[$"{smtpSetting}:{SmtpSettings.Password}"];

                var smtpClient = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username, "Support"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(emailToResive);

                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Generates a reset token for a user, deletes any existing tokens, and sends an email with the new token.
        /// </summary>
        /// <param name="email">The email address of the user to generate the token for.</param>
        /// <returns>
        /// True if the token is generated and the email is sent successfully; false otherwise.
        /// </returns>
        /// <exception cref="Exception">Thrown if token generation or email sending fails.</exception>
        public async Task<bool> GenerateTokenAndSendResetEmail(string email)
        {
            try
            {
                await _passwordRepo.ValidateAndDeleteEmail(email);

                await _tokenGeneratorRepo.CreateTokenAsync(email);

                string token = await _tokenGeneratorRepo.GetTokenAsync(email);

                if (token == null)
                {
                    throw new Exception("Failed to generate token");
                }

                string subject = "Nyt password?";
                string body = $@"
                <p>Din kode: {token}</p>
                <p>Koden er gyldig i 30 min.</p>
                <br/>
                <p></p>
                <p></p>
                <p>Mvh.</p>
                <p>Support.</p>
                <p>Denne mail kan bliver ikke besvaret</p>";

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
