using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, string cardNumber, string lastTransactionNumber, DateTime date, 
        DateTime expireDate, 
        decimal total, 
        decimal totalPaid, 
        Address address, 
        Document document, 
        string payer, 
        Email email):base( date, 
         expireDate, 
         total, 
         totalPaid, 
         address, 
         document, 
         payer, 
         email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }

    
}