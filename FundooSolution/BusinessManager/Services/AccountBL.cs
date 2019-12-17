namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.Models;
    using NotesRepository.Interface;
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
            if (model != null)
            {
                string result = notesRL.ForgotPassword(model);
                return "token:"+result;
            }
            else
            {
                return "Email Should not be empty";
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string Login(LoginRequestModel model)
        {
            if (model != null)
            {
                string token = notesRL.Login(model);
                if (token != string.Empty)
                {
                    return "Login successful!! " + "Token :" + token;
                }
                else
                {
                    return "Invalid credentials";
                }
            }
            else
            {
                return "Invalid credentials";
            }
        }

        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<string> RegisterAsync(RegisterRequestModel model)
        {
            if (model != null)
            {
                string result = await notesRL.RegisterAsync(model);
                return result;
            }
            else
            {
                return "Insufficients details....";
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public string ResetPassword(ResetPasswordModel token)
        {
            string result = notesRL.ResetPassword(token);
            return "result : "+result;
        }
    }
}
