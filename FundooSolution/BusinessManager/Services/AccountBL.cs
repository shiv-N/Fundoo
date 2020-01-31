namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.AccountModels.Response;
    using CommonLayerModel.Models;
    using Microsoft.AspNetCore.Http;
    using NotesRepository.Interface;
    using ServiceStack.Redis;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is class AccountBL
    /// </summary>
    /// <seealso cref="BusinessManager.Interface.IAccountBL" />
    public class AccountBL : IAccountBL
    {
        /// <summary>
        /// The notesRL
        /// </summary>
        IAccountRL notesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBL"/> class.
        /// </summary>
        /// <param name="notesRL">The notes rl.</param>
        public AccountBL(IAccountRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string ForgotPassword(ForgotPassword model)
         {
            string Token = notesRL.ForgotPassword(model);
            return Token;
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
                AccountLoginResponce loginResponce = notesRL.Login(model);
                if (loginResponce.Token != null)
                {
                    RedisEndpoint redisEndpoint = new RedisEndpoint("localhost", 6379);
                    using (RedisClient client = new RedisClient(redisEndpoint))
                    {
                        if (client.Get<string>(model.Email + model.Password)==null)
                        {
                            client.Set<string>(model.Email + model.Password, DateTime.Now.ToString());
                            loginResponce.LoginTime = client.Get<string>(model.Email + model.Password);
                        }
                        else
                        {
                            client.Remove(model.Email + model.Password);
                            client.Set<string>(model.Email + model.Password, DateTime.Now.ToString());
                            loginResponce.LoginTime = client.Get<string>(model.Email + model.Password);
                        }
                    }
                }
                return loginResponce;  
            }
            catch(Exception e)
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
                AccountLoginResponce loginResponce = await notesRL.AdminLogin(model);
                if (loginResponce.Token != null)
                {
                    RedisEndpoint redisEndpoint = new RedisEndpoint("localhost", 6379);
                    using (RedisClient client = new RedisClient(redisEndpoint))
                    {
                        if (client.Get<string>(model.Email + model.Password) == null)
                        {
                            client.Set<string>(model.Email + model.Password, DateTime.Now.ToString());
                            loginResponce.LoginTime = client.Get<string>(model.Email + model.Password);
                        }
                        else
                        {
                            client.Remove(model.Email + model.Password);
                            client.Set<string>(model.Email + model.Password, DateTime.Now.ToString());
                            loginResponce.LoginTime = client.Get<string>(model.Email + model.Password);
                        }
                    }
                }
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
        public async Task<bool> RegisterAsync(RegisterRequestModel model)
        {
            try
            {
                return await notesRL.RegisterAsync(model);
            }
            catch(Exception e)
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
                return await notesRL.AdminRegisterAsync(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<GetAllUserResponce>> GetAllUserByAdminAuthorization()
        {
            try
            {
                return await notesRL.GetAllUserByAdminAuthorization();
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
        public async Task<Dictionary<string,int>> GetUserStatisticsByAdmin()
        {
            try
            {
                return await notesRL.GetUserStatisticsByAdmin();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public bool ResetPassword(ResetPasswordModel token,int userId)
        {
            return notesRL.ResetPassword(token, userId);
        }
        public async Task<string> AddProfilePhoto(IFormFile file,int userId)
        {
            return await notesRL.AddProfilePhoto(file, userId);
        }
    }
}
