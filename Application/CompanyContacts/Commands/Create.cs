namespace Application.CompanyContacts.Commands
{
    using FluentValidation;
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    public class Create
    {
        public class Command : IRequest
        {
            public Guid CompanyId { get; set; }
            public Guid ContactId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CompanyId).NotEmpty();
                RuleFor(x => x.ContactId).NotEmpty();
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
                var companyContacts = new CompanyContacts
                {
                    CompanyId = request.CompanyId,
                    ContactId = request.ContactId
                };

                _context.CompanyContacts.Add(companyContacts);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}
