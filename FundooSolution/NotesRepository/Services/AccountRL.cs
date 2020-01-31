using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayerModel.AccountModels;
using CommonLayerModel.AccountModels.Response;
using CommonLayerModel.Models;
using CommonLayerModel.MSMQ;
using Microsoft.AspNetCore.Http;
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
        MsmqSender msmq;
        public AccountRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> AddProfilePhoto(IFormFile file, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                Account account = new Account(configuration["Data:CloudName"], configuration["Data:API_Key"], configuration["Data:API_Secret"]);
                var Path = file.OpenReadStream();
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, Path),
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                SqlCommand command = StoreProcedureConnection("spUpdateProfileImage", connection);
                command.Parameters.AddWithValue("@ProfileImage", uploadResult.Uri.ToString());
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return uploadResult.Uri.ToString();
                }
                else
                {
                    return null;
                };
            }
            catch (Exception e)
            {
                throw e;
            }
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
                msmq = new MsmqSender();
                msmq.SendToMsmq(token, model.Email);
                return token;
            }
            return string.Empty;
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AccountLoginResponce Login(LoginRequestModel model)
        {
            try
            {
                SqlConnection connection = DBConnection();
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spUserLogin", connection);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Password", encrypted);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                AccountLoginResponce loginResponce = new AccountLoginResponce();
                while (dataReader.Read())
                {
                    if ((dataReader["Email"].ToString()).Equals(model.Email))
                    {
                        loginResponce.Id = (int)dataReader["Id"];
                        loginResponce.FirstName = dataReader["FirstName"].ToString();
                        loginResponce.LastName = dataReader["LastName"].ToString();
                        loginResponce.Email = dataReader["Email"].ToString();
                        loginResponce.PhoneNumber = dataReader["PhoneNumber"].ToString();
                        loginResponce.UserAddress = dataReader["UserAddress"].ToString();
                        loginResponce.ServiceType = dataReader["ServiceType"].ToString();
                        loginResponce.UserType = dataReader["UserType"].ToString();
                        loginResponce.Token = GenrateJWTToken(model.Email, loginResponce.Id);
                        loginResponce.ProfilePhoto = dataReader["ProfilePhoto"].ToString();
                        break;
                    }
                }
                connection.Close();
                return loginResponce;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<AccountLoginResponce> AdminLogin(LoginRequestModel model)
        {
            try
            {
                SqlConnection connection = DBConnection();
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spAdminLogin", connection);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Password", encrypted);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                AccountLoginResponce loginResponce = new AccountLoginResponce();
                while (dataReader.Read())
                {
                    if ((dataReader["Email"].ToString()).Equals(model.Email))
                    {
                        loginResponce.Id = (int)dataReader["Id"];
                        loginResponce.FirstName = dataReader["FirstName"].ToString();
                        loginResponce.LastName = dataReader["LastName"].ToString();
                        loginResponce.Email = dataReader["Email"].ToString();
                        loginResponce.PhoneNumber = dataReader["PhoneNumber"].ToString();
                        loginResponce.UserAddress = dataReader["UserAddress"].ToString();
                        loginResponce.ServiceType = dataReader["ServiceType"].ToString();
                        loginResponce.UserType = dataReader["UserType"].ToString();
                        loginResponce.Token = GenrateJWTToken(model.Email, loginResponce.Id);
                        break;
                    }
                }
                connection.Close();
                return loginResponce;
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
                SqlCommand command = StoreProcedureConnection("spRegisterUser", connection);
                command.Parameters.AddWithValue("FirstName", model.FirstName);
                command.Parameters.AddWithValue("LastName", model.LastName);
                command.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("Email", model.Email);
                command.Parameters.AddWithValue("Password", encrypted);
                command.Parameters.AddWithValue("UserAddress", model.UserAddress);
                command.Parameters.AddWithValue("ServiceType", model.ServiceType);
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

        /// <summary>
        /// Admins the register asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> AdminRegisterAsync(RegisterRequestModel model)
        {
            try
            {
                SqlConnection connection = DBConnection();
                string encrypted = EncryptPassword(model.Password);
                SqlCommand command = StoreProcedureConnection("spRegisterAdmin", connection);
                command.Parameters.AddWithValue("FirstName", model.FirstName);
                command.Parameters.AddWithValue("LastName", model.LastName);
                command.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("Email", model.Email);
                command.Parameters.AddWithValue("Password", encrypted);
                command.Parameters.AddWithValue("UserAddress", model.UserAddress);
                command.Parameters.AddWithValue("ServiceType", model.ServiceType);
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

        /// <summary>
        /// Databases the connection.
        /// </summary>
        /// <returns></returns>
        private SqlConnection DBConnection()
        {
            return new SqlConnection(configuration["Data:ConnectionString"]);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public bool ResetPassword(ResetPasswordModel token, int userId)
        {
            SqlConnection connection = DBConnection();
            //token = DecodeToken(token);
            string encrypted = EncryptPassword(token.Password);
            List<SpParameterData> paramsList = new List<SpParameterData>();
            //paramsList.Add(new SpParameterData("@UserId", userId));
            //DataTable table = spExecuteReader("spDisplayNotesByUserId", paramsList);
            SqlCommand command = StoreProcedureConnection("spUpdatePasswordByEmail", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Password", encrypted);
            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets all user by admin authorization.
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetAllUserResponce>> GetAllUserByAdminAuthorization()
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spGetUserList", connection);
                List<GetAllUserResponce> responces = new List<GetAllUserResponce>();
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    GetAllUserResponce get = new GetAllUserResponce();
                    get.Id = (int)dataReader["Id"];
                    get.FirstName = dataReader["FirstName"].ToString();
                    get.LastName = dataReader["LastName"].ToString();
                    get.Email = dataReader["Email"].ToString();
                    get.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    get.UserAddress = dataReader["UserAddress"].ToString();
                    get.ServiceType = dataReader["ServiceType"].ToString();
                    get.UserType = dataReader["UserType"].ToString();
                    responces.Add(get);
                }
                connection.Close();
                return responces;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the user statistics by admin.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetUserStatisticsByAdmin()
        {
            SqlConnection connection = DBConnection();
            SqlCommand command = StoreProcedureConnection("spUserstatistics", connection);
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            connection.Open();
            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            int basicCount = 0, advanceCount = 0;
            while (dataReader.Read())
            {
                if(dataReader["ServiceType"].ToString().Equals("Advance") || dataReader["ServiceType"].ToString().Equals("advance"))
                {
                    advanceCount++;
                }
                else if(dataReader["ServiceType"].ToString().Equals("Basic") || dataReader["ServiceType"].ToString().Equals("basic"))
                {
                    basicCount++;
                }
            }
            connection.Close();
            keyValues.Add("Advance", advanceCount);
            keyValues.Add("Basic", basicCount);
            return keyValues;
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
        //private static ResetPasswordModel DecodeToken(ResetPasswordModel token)
        //{
        //    var stream = token.Token;
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(stream);
        //    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
        //    token.Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "email").Value;
        //    token.Id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "id").Value;
        //    return token;
        //}
        private async Task<DataTable> spExecuteReader(string spName, IList<SpParameterData> spParams)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection(spName, connection);
                for (int i = 0; i < spParams.Count; i++)
                {
                    command.Parameters.AddWithValue(spParams[i].name, spParams[i].value);
                }
                connection.Open();
                DataTable table = new DataTable();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                table.Load(dataReader);
                connection.Close();
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        class SpParameterData
        {
            public SpParameterData(string name, dynamic value)
            {
                this.name = name;
                this.value = value;
            }
            public string name { get; set; }
            public dynamic value { get; set; }
        }
    }
}
