using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interface;
using CommonLayerModel.AccountModels.Response;
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
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    if (await account.RegisterAsync(model))
                    {
                        return Ok(new { Success = true, Message = "User is Registered SuccessFully", model });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "User is not Registered", model });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Insufficients details....", model });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { Success = false, Message = e.Message });
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
            try
            {
                if (model != null)
                {
                    AccountLoginResponce Data = account.Login(model);
                    if (Data.Token != null)
                    {
                        return Ok(new { Success = true, Message = "Login successful!!", Data });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Wrong Email or Password" });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message ="Invalid credentials"});
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, Message = e.Message });
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
            string Token;
            if (model != null)
            {
                Token = account.ForgotPassword(model);
                if (Token != string.Empty)
                {
                    var passwordRestLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = Token }, Request.Scheme);
                    return Ok(new { Suceess = true, Meassage = "Forgot password conformation", Token, model });
                }
                else
                {
                    return BadRequest(new { Suceess=false, Meassage = "Invalid email" });
                }
            }
            else
            {
                return BadRequest(new { Suceess = false, Meassage = "Email Should not be empty" });
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
            var userId = TokenUserId();
            //var userEmailId = TokenUserEmail();
            if (model != null)
            {
                if (account.ResetPassword(model, userId))
                {
                    return Ok(new { success=true,Message= "reset password conformation" });
                }
                else
                {
                    return BadRequest(new { Suceess = false, Meassage = "reset password is not conformation" });
                }
            }
            else
            {
                return BadRequest(new { Suceess = false, Meassage = "Invalid Credential" });
            }
        }
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("Id").Value);
        }
        //private string TokenUserEmail()
        //{
        //   // return User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

        //    string email = HttpContext.User.Claims.First(e => e.Type == "email").Value;
        //    return email;
        //}
    }
}