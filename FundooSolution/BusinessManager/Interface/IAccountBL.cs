﻿namespace BusinessManager.Interface
{
    using CommonLayerModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is interface IAccountBL
    /// </summary>
    public interface IAccountBL
    {
        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> RegisterAsync(RegisterRequestModel model);

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string Login(LoginRequestModel model);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string ForgotPassword(ForgotPassword model);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        ResetPasswordModel ResetPassword(ResetPasswordModel token);
    }
}
