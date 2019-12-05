using BusinessManager.Interface;
using CommonLayerModel.Models;
using NotesRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class AccountBL : IAccountBL
    {
        IAccountRL notesRL;
        public AccountBL(IAccountRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public string ForgotPassword(ForgotPassword model)
        {
            return notesRL.ForgotPassword(model);
        }

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

        public string ResetPassword(ResetPasswordModel token)
        {
            return notesRL.ResetPassword(token);
        }
    }
}
