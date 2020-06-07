namespace Application.Campaign.Commands
{
    using MediatR;
    using System;
    using Application.Errors;
    using Domain;
    using FluentValidation;
    using Persistence;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public Guid CompanyId { get; set; }
            public Company Company { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
                RuleFor(x => x.CompanyId).NotEmpty();
                RuleFor(x => x.Company).NotEmpty();
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
                var campaign = await _context.Campaigns.FindAsync(request.Id);

                if (campaign == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Campaign = "Not Found" });

                campaign.Name = request.Name ?? campaign.Name;
                campaign.Description = request.Description ?? campaign.Description;
                campaign.Company = request.Company ?? campaign.Company;
                campaign.StartDate = request.StartDate ?? campaign.StartDate;
                campaign.EndDate = request.EndDate ?? campaign.EndDate;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
