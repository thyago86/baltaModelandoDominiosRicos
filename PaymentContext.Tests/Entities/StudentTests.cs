using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests(){
            _name = new Name("Bruce", "Wayne");
            _document = new Document("41361299045", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription(){
            var payment = new PayPalPayment("12345678",DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Wayne Corp", _email );

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment(){
            
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }
        

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription(){
            var payment = new PayPalPayment("12345678",DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Wayne Corp", _email );
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
    }
}
