using CommonLayerModel.AccountModels;
using CommonLayerModel.Models;
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
    public class AccountRL : IAccountRL
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localDB)\localhost;Initial Catalog=EmployeeDetails;Integrated Security=True");

       // private readonly ApplicationSettings applicationSettings;

        //public AccountRL(IOptions<ApplicationSettings> applicationSettings)
        //{
        //    this.applicationSettings = applicationSettings;
        //}


        public string ForgotPassword(ForgotPassword model)
        {
            string email = model.Email;
            SqlCommand command = StoreProcedureConnection("spForgotPassword");
            command.Parameters.AddWithValue("@Email", model.Email);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int id = (int)dataReader["Id"];
                string token = GenrateJWTToken(email,id);
                //msmq = new MsmqSender();
                //msmq.SendToMsmq(token, model.Email);
                return "ForgotPasswordConformation, token : " + token;
            }
            return string.Empty;
        }

        public string Login(LoginRequestModel model)
        {
            try
            {
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spLogin");
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

        public async Task<string> RegisterAsync(RegisterRequestModel model)
        {
            try
            {
                if (model.Password == null)
                {
                    throw new ArgumentNullException("Password");
                }
                else
                {
                    string encrypted = EncryptPassword(model.Password);
                    SqlCommand command = StoreProcedureConnection("spInsertUser");
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
                        return "User is Registered SuccessFully";
                    }
                    else
                    {
                        return "Data is not Registered";
                    }
                }
            }
            catch (Exception e)
            {
                return "Data is not Registered. it throws exception as following.\n" + e.Message;
            }
        }

        public string ResetPassword(ResetPasswordModel token)
        {
            token = DecodeToken(token);
            string encrypted = EncryptPassword(token.Password);
            SqlCommand command = StoreProcedureConnection("spUpdatePasswordByEmail");
            command.Parameters.AddWithValue("@Email", token.Email);
            command.Parameters.AddWithValue("@Password", encrypted);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            connection.Close();
            return "Email:"+token.Email+" Id: "+token.Id;
        }
        private SqlCommand StoreProcedureConnection(string Name)
        {
            SqlCommand command = new SqlCommand(Name, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
        private static string EncryptPassword(string Password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
            String encrypted = Convert.ToBase64String(encrypt);
            return encrypted;
        }
        private static string GenrateJWTToken(string email,int id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg".ToString()));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            ////new Claim(ClaimTypes.Role, "User")
                            new Claim("id",userId)
                        };
            var tokenOptionOne = new JwtSecurityToken(
              
                claims: claims,
                expires: DateTime.Now.AddMinutes(130),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
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
