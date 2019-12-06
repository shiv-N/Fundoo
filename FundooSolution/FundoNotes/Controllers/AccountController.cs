using BusinessManager.Interface;
using CommonLayerModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]

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
            return Ok(await account.RegisterAsync(model));
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
            return Ok(account.Login(model));

        }

        /// <summary>
        /// this is mehtod ForgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgot")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPassword model)
        {
            string userToken = account.ForgotPassword(model);
            if (userToken != string.Empty)
            {
                var passwordRestLink = Url.Action("ResetPassword", "Account",
                    new { email = model.Email, token = userToken }, Request.Scheme);
                return Ok(userToken);
            }
            else
            {
                return Ok("Invalid email");
            }
        }

        /// <summary>
        /// this is method ResetPassword
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("reset")]
        [HttpPost]
      
        public IActionResult ResetPassword(ResetPasswordModel token)
        {
            return Ok(account.ResetPassword(token));
        }
    }
}