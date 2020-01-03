namespace NotesRepository.Interface
{
    using CommonLayerModel.AccountModels.Response;
    using CommonLayerModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is interface IAccountRL
    /// </summary>
    public interface IAccountRL
    {
        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> RegisterAsync(RegisterRequestModel model);

        Task<bool> AdminRegisterAsync(RegisterRequestModel model);

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        AccountLoginResponce Login(LoginRequestModel model);
        Task<AccountLoginResponce> AdminLogin(LoginRequestModel model);

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
        bool ResetPassword(ResetPasswordModel token,int userId);
        Task<List<GetAllUserResponce>> GetAllUserByAdminAuthorization();
        Task<Dictionary<string, int>> GetUserStatisticsByAdmin();
    }
}
