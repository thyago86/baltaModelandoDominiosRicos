using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;          
           _subscriptions = new List<Subscription>();

           AddNotifications(name, document, email);
        }

        public Name Name { get; private set;}
        
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Address { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions {get{return _subscriptions.ToArray(); }}

        public void AddSubscription(Subscription subscription)
        {
            //se já tiver uma assinatura, cancela
            //Cancela as outras assinaturas, coloca esta como principal
            if(subscription.Payments.Count == 0){

            }
            
            var hasSubscriptionActive = false;
            foreach(var sub in _subscriptions){
                if(sub.Active)
                    hasSubscriptionActive = true;
            }
            AddNotifications(new Contract()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscription", "Você já tem uma assinatura ativa")
            .AreEquals(0, subscription.Payments.Count, "Students.Subscription.Payments", "Esta assinatura não possui pagamentos."));

            // if(hasSubscriptionActive){
            //     AddNotification("Student.Subscription", "Você já tem uma assinatura ativa");
            // }
        }
    }
}