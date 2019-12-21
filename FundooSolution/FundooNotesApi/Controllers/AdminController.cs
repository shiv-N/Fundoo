using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interface;
using CommonLayerModel.AccountModels.Response;
using CommonLayerModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAccountBL account;

        public AdminController(IAccountBL account)
        {
            this.account = account;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminRegistration(RegisterRequestModel register)
        {
            try
            {

                if (register != null)
                {
                    if (await account.AdminRegisterAsync(register))
                    {
                        return Ok(new { Success = true, Message = "Admin is Registered SuccessFully", register });
                    }
                    else
                    {
                        return Ok(new { Success = false, Message = "Admin is not Registered", register });
                    }
                }
                else
                {
                    return Ok(new { Success = false, Message = "Insufficients details....", register });
                }
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Message = e.Message });
            }
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    AccountLoginResponce Data = await account.AdminLogin(model);
                    if (Data.Token != null)
                    {
                        return Ok(new { Success = true, Message = "Admin Login successful!!", Data });
                    }
                    else
                    {
                        return Ok(new { Success = false, Message = "Invalid credentials" });
                    }
                }
                else
                {
                    return Ok(new { Success = false, Message = "Invalid credentials" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Message = e.Message });
            }

        }

        [HttpGet("User")]
        public async Task<IActionResult> GetAllUserByAdminAuthorization()
        {
            try
            {
                List<GetAllUserResponce> Data = await account.GetAllUserByAdminAuthorization();
                if(Data != null)
                {
                    return Ok(new { Success = true, Message = "Get All User successful!!", Data });
                }
                else
                {
                    return Ok(new { Success = false, Message = "Get All User unsuccessful!!" });
                }
            }
            catch(Exception e)
            {
                return Ok(new { Success = false, Message = e.Message });
            }
        }

        [HttpGet("UserStatistics")]
        public async Task<IActionResult> GetUserStatisticsByAdmin()
        {
            try
            {
                Dictionary<string, int> Data = await account.GetUserStatisticsByAdmin();
                if (Data != null)
                {
                    return Ok(new { Success = true, Message = "Get User Statistics successful!!", Data });
                }
                else
                {
                    return Ok(new { Success = false, Message = "Get User Statistics unsuccessful!!" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Message = e.Message });
            }
        }
    }
}