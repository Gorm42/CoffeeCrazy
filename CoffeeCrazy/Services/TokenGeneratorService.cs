﻿using CoffeeCrazy.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeCrazy.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken()
        {
            StringBuilder token = new StringBuilder();
            int nr;
            for (int i = 0; i < 6; i++)
            {
                nr = RandomNumberGenerator.GetInt32(9);
                token.Append(Convert.ToString(nr));
            }
            return token.ToString();
        }
    }
}
