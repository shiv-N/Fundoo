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
            string Token = notesRL.ForgotPassword(model);
            return Token;
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string Login(LoginRequestModel model)
        {
            string token = notesRL.Login(model);
            return  token;  
        }

        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(RegisterRequestModel model)
        {
            return await notesRL.RegisterAsync(model);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public ResetPasswordModel ResetPassword(ResetPasswordModel token)
        {
            return notesRL.ResetPassword(token);
        }
    }
}
