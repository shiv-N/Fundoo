using BusinessManager.Services;
using CommonLayerModel.Models;
using Moq;
using NotesRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FundooTest.UserAccount
{
    public class AccountTesting
    {
        [Fact]
        public void AccountRegistration()
        {
            AccountBL business = objectIntialize();

            var modelRegister = new RegisterRequestModel()
            {

                FirstName = "shiv",
                LastName = "charan",
                PhoneNumber = "6423894630",
                Email = "abc@gmail.com",
                Password = "abc@123",
                UserAddress = "mumbai"

            };

            var dataRegister = business.RegisterAsync(modelRegister);
            Assert.NotNull(dataRegister);
        }

        [Fact]
        public void AccountLogin()
        {
            AccountBL business = objectIntialize();

            var modelLogin = new LoginRequestModel()
            {
                Email = "shiva@Gmail.com",
                Password = "QWERTY@123"
            };
            var dataLogin = business.Login(modelLogin);
            Assert.NotNull(dataLogin);
        }
        [Fact]
        public void AccountForgotPassword()
        {
            AccountBL business = objectIntialize();

            var modelForgotPassword = new ForgotPassword()
            {
                Email = "shiva@Gmail.com"
            };
            var dataForgotPassword = business.ForgotPassword(modelForgotPassword);
            Assert.NotNull(dataForgotPassword);
        }
        private static AccountBL objectIntialize()
        {
            var repository = new Mock<IAccountRL>();
            var business = new AccountBL(repository.Object);
            return business;
        }

    }
}
