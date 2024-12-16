using CoffeeCrazy.Interfaces;
using Microsoft.Data.SqlClient;

namespace CoffeeCrazy.Services
{
    public class ValidationServices : IValidationService
    {
        private readonly string _connectionString;
        public ValidationServices(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Metode til at sammenligne den skrevne email med hvad der allerede ligger i databasen.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns> Returnerer en true, hvis email allerede eksisterer i databasen. False hvis emailen ikke eksisterer. </returns>
        public async Task<bool> CompareEmailsAsync(string userEmail)
        {

            List<string> databaseEmails = await RetrieveAllEmails();

            foreach (string email in databaseEmails)
            {
                if (userEmail.ToLower() == email.ToLower())
                {
                    return true;
                }    
            }
            return false;
        }

        /// <summary>
        /// Metode til at hente ALLE email fra User tabellen i databasen.
        /// </summary>
        /// <returns> En liste med string attributter af typen Email, der skal bruge til at validere bruger input helt oppe i frontend-razorpages </returns>
        public async Task<List<string>> RetrieveAllEmails()
        {
            List<string> allEmails = new List<string>(); //Listen vi skal gemme alle vores database emails i.
            try
            {


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Email FROM Users"; //SQL til at hente ALLE emails der ligger under User tabellen.

                    SqlCommand sqlCommand = new SqlCommand(query, connection);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            allEmails.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return allEmails;

        }
    }
}
