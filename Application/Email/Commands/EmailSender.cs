namespace Application.Email.Commands
{
    using FluentValidation;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmailSender
    {
        public class Command : IRequest
        {
            public Guid CampaignId { get; set; }
            public Guid CompanyId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CompanyId).NotEmpty();
                RuleFor(x => x.CampaignId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IEmailSender _emailSender;

            public Handler(DataContext context, IEmailSender emailSender)
            {
                _context = context;
                _emailSender = emailSender;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var campaign = await _context.Campaigns.FindAsync(request.CampaignId);
                var companyContacts = await _context.Contacts.Where(x => x.CompanyId == request.CompanyId).ToListAsync(cancellationToken);
                if (companyContacts.Any())
                {
                    foreach (var company in companyContacts)
                    {
                        await _emailSender.SendEmailAsync(company.Email, campaign.Name, campaign.Description).ConfigureAwait(false);
                    }
                }
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
