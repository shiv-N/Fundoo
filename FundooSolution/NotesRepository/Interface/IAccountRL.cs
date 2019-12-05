using CommonLayerModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesRepository.Interface
{
    public interface IAccountRL
    {
        Task<string> RegisterAsync(RegisterRequestModel model);
        string Login(LoginRequestModel model);
        string ForgotPassword(ForgotPassword model);
        string ResetPassword(ResetPasswordModel token);
    }
}
