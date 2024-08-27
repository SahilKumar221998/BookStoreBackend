using Business.Exceptions;
using Business.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Entites;
using Model.Request;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRepo;
        private readonly IConfiguration _configuration;
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterartions = 10000;

        public UserBL(IUserRL userRepo, IConfiguration _configuration)
        {
            this.userRepo = userRepo;
            this._configuration = _configuration;
        }
        public string passwordHashing(string userPass)
        {
            try
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
                var pkdf2 = new Rfc2898DeriveBytes(userPass, salt, Iterartions);
                byte[] hash = pkdf2.GetBytes(HashSize);

                // Combine salt and hash into a single byte array
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Convert the combined byte array to a base64-encoded string for storage
                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                return savedPasswordHash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyPassword(string userPass, string storedHashPass)
        {
            // Convert the stored password hash from base64 back to bytes
            byte[] hashBytes = Convert.FromBase64String(storedHashPass);

            // Extract salt from the stored password hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Compute the hash of the entered password using the same salt and iterations
            var pbkdf2 = new Rfc2898DeriveBytes(userPass, salt, Iterartions);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
        public bool createUser(UserRequest request)
        {
            return userRepo.createUser(MapToEntity(request));
        }

        private User MapToEntity(UserRequest request)
        {
            return new User
            {
                name = request.name,
                email = request.email,
                password = passwordHashing(request.password),
                mobileNumber = request.mobileNumber

            };
        }
        public string[] login(string userEmail, string password)
        {
            try
            {
                User user = userRepo.login(userEmail);
                if (user == null)
                {
                    throw new UserNotFoundException("User Not Present in DataBase please register");
                }
                else
                {
                    if (VerifyPassword(password, user.password))
                        return new string[] { GenerateJwtToken(user), user.name };
                    else
                        throw new PasswordMissMatchException("incorrect Password Entered By User");
                }
            }
            catch (InvalidOperationException)
            {
                throw new UserNotFoundException("UserNot in DataBase please register");

            }
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._configuration[("JwtSettings:Authorization")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim("userId",user.userId.ToString()),
                     new Claim(ClaimTypes.Email, user.email),
                     new Claim(ClaimTypes.Name,user.name)

                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
