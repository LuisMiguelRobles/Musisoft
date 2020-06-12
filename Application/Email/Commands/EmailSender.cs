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
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CampaignId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IEmailSender _emailSender;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IEmailSender emailSender, IUserAccessor userAccessor)
            {
                _context = context;
                _emailSender = emailSender;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var campaign = await _context.Campaigns.FindAsync(request.CampaignId);
                var contacts = await _context.Contacts.Where(x => x.User.UserName == _userAccessor.GetCurrentUsername()).ToListAsync(cancellationToken);
                if (contacts.Any())
                {
                    foreach (var contact in contacts)
                    {
                        await _emailSender.SendEmailAsync(contact.Email, campaign.Name, campaign.Description).ConfigureAwait(false);
                    }
                }
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
