using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interface;
using CommonLayerModel.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesRepository.Interface;

namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBL account;

        /// <summary>
        /// this is method AccountController
        /// </summary>
        /// <param name="account"></param>
        public AccountController(IAccountBL account)
        {
            this.account = account;
        }

        /// <summary>
        /// this is method Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel model)
        {
            string status;
            if (model != null)
            {
                if(await account.RegisterAsync(model))
                {
                    status = "User is Registered SuccessFully";
                    return Ok(new { status, model });
                }
                else
                {
                    status = "Data is not Registered";
                    return Ok(new { status, model });
                }
            }
            else
            {
                status = "Insufficients details....";
                return Ok(new { status, model });
            }
        }

        /// <summary>
        /// this is method Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestModel model)
        {
            string status,token;
            if (model != null)
            {
                token = account.Login(model);
                if (token != string.Empty)
                {
                    status = "Login successful!! ";
                    return Ok(new { status, token, model });
                }
                else
                {
                    status = "Invalid credentials";
                    return Ok(new { status, model });
                }
            }
            else
            {
                status = "Invalid credentials";
                return Ok(new { status, model });
            }

        }

        /// <summary>
        /// this is mehtod ForgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns> IActionResult result</returns>
        [HttpPost("forgot")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPassword model)
        {
            string status, Token;
            if (model != null)
            {
                Token = account.ForgotPassword(model);
                if (Token != string.Empty)
                {
                    var passwordRestLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = Token }, Request.Scheme);
                    status = "Forgot password conformation";
                    return Ok(new { status, Token, model });
                }
                else
                {
                    status = "Invalid email";
                    return Ok(new { status, model });
                }
            }
            else
            {
                status = "Email Should not be empty";
                return Ok(new { status,model });
            }
        }

        /// <summary>
        /// this is method ResetPassword
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("reset")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            string status;
            if (model != null)
            {
                ResetPasswordModel responseModel = account.ResetPassword(model); ;
                if (responseModel.Id != string.Empty)
                {
                    status = "reset password conformation";
                    return Ok(new { status, responseModel });
                }
                else
                {
                    status = "reset password is not conformation";
                    return Ok(new { status, model });
                }
            }
            else
            {
                status = "Invalid Credential";
                return Ok(new { status, model });
            }
        }
    }
}