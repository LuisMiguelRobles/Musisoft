using System;

namespace Application.Email.Commands
{
    using Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmailSender
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEmailSender _emailSender;

            public Handler(IEmailSender emailSender)
            {
                _emailSender = emailSender;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _emailSender.SendEmailAsync("luismirobles97@gmail.com", "test", "Este es un test").ConfigureAwait(false);
                
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
