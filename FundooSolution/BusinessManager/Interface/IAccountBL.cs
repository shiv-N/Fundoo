using CommonLayerModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Interface
{
    public interface IAccountBL
    {
        Task<string> RegisterAsync(RegisterRequestModel model);
        string Login(LoginRequestModel model);
        string ForgotPassword(ForgotPassword model);
        string ResetPassword(ResetPasswordModel token);
    }
}
