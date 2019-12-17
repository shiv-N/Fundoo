using CommonLayerModel.AccountModels;
using CommonLayerModel.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace NotesRepository.Services
{
    /// <summary>
    /// this is class AccountRL
    /// </summary>
    /// <seealso cref="NotesRepository.Interface.IAccountRL" />
    public class AccountRL : IAccountRL
    {
        IConfiguration configuration;
        public AccountRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string ForgotPassword(ForgotPassword model)
        {
            SqlConnection connection = DBConnection();
            string email = model.Email;
            SqlCommand command = StoreProcedureConnection("spForgotPassword", connection);
            command.Parameters.AddWithValue("@Email", model.Email);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int id = (int)dataReader["Id"];
                string token = GenrateJWTToken(email,id);
                //msmq = new MsmqSender();
                //msmq.SendToMsmq(token, model.Email);
                return token;
            }
            return string.Empty;
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string Login(LoginRequestModel model)
        {
            try
            {
                SqlConnection connection = DBConnection();
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spLogin", connection);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Password", encrypted);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                string token = string.Empty;
                while (dataReader.Read())
                {
                    if ((dataReader["Email"].ToString()).Equals(model.Email))
                    {
                        int Id = (int)dataReader["Id"];
                        token = GenrateJWTToken(model.Email, Id);
                        break;
                    }
                }
                connection.Close();
                return token;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Password</exception>
        public async Task<bool> RegisterAsync(RegisterRequestModel model)
        {
            try
            {
                SqlConnection connection = DBConnection();
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spInsertUser", connection);
                command.Parameters.AddWithValue("FirstName", model.FirstName);
                command.Parameters.AddWithValue("LastName", model.LastName);
                command.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("Email", model.Email);
                command.Parameters.AddWithValue("Password", encrypted);
                command.Parameters.AddWithValue("UserAddress", model.UserAddress);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private SqlConnection DBConnection()
        {
            return new SqlConnection(configuration["Data:ConnectionString"]);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public ResetPasswordModel ResetPassword(ResetPasswordModel token)
        {
            SqlConnection connection = DBConnection();
            token = DecodeToken(token);
            string encrypted = EncryptPassword(token.Password);
            SqlCommand command = StoreProcedureConnection("spUpdatePasswordByEmail", connection);
            command.Parameters.AddWithValue("@Email", token.Email);
            command.Parameters.AddWithValue("@Password", encrypted);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if (result == 0)
            {
                token.Id = string.Empty;
            }
            connection.Close();
            return token;
        }

        /// <summary>
        /// Stores the procedure connection.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        private SqlCommand StoreProcedureConnection(string Name, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(Name, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        private static string EncryptPassword(string Password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
            String encrypted = Convert.ToBase64String(encrypt);
            return encrypted;
        }

        /// <summary>
        /// Genrates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private string GenrateJWTToken(string email, int id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Data:key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            ////new Claim(ClaimTypes.Role, "User")
                            new Claim("id",userId),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(130),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }

        /// <summary>
        /// Decodes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private static ResetPasswordModel DecodeToken(ResetPasswordModel token)
        {
            var stream = token.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            token.Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "email").Value;
            token.Id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "id").Value;
            return token;
        }

    }
}
