using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        //Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "hello@balta.io2";

            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";

            command.PaymentNumber = "123231";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Payer = "Wayne Corp";
            command.PayerEmail = "batman@dc.com";
            command.Street = "asad";
            command.Number = "das";
            command.Neighborhood = "dasdsa";
            command.City = "asdads";
            command.State = "asd";
            command.Country = "asdsad";
            command.ZipCode = "12345678";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);

        }

    }
}