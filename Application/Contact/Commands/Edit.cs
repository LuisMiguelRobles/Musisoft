namespace Application.Contact.Commands
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Errors;
    using FluentValidation;
    using MediatR;
    using Persistence;

    public class Edit
    {
        public class Command: IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

        }

        public class CommandValidator: AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var contact = await _context.Contacts.FindAsync(request.Id);

                if (contact == null)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not found" });

                contact.Email = request.Email ?? contact.Email;
                contact.Name = request.Name ?? contact.Name;
                contact.LastName = request.LastName ?? contact.LastName;


                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}
