using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers{
    public class SubscriptionHandler :
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validations
            command.Validate();
            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta uso");
            
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já esta em uso");


            
            
            //gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            //Gerar as entidades

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, 
            command.BoletoNumber, 
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid, 
            address, 
            new Document(command.PayerDocument, command.PayerDocumentType), 
            command.Payer, email);

            //Relacionamentos
            AddNotifications(name, document, email, address, student, subscription, payment);
            //aplicar as validações
            //salvar as validações
            _repository.CreateSubscription(student);

            //enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");
            //retornar informações
            AddNotifications(new Contract());
            return new CommandResult(true, "Assinatura finalizada com sucesso");
        }
    }
}