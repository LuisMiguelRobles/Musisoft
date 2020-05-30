namespace Application.Company.Commands
{
    using Domain;
    using FluentValidation;
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Nit { get; set; }
            public string Name { get; set; }
            public string AppUserId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Nit).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.AppUserId).NotEmpty();
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
                var company = new Company
                {
                    Name = request.Name,
                    Nit = request.Nit,
                    AppUserId = request.AppUserId
                };

                _context.Companies.Add(company);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}
