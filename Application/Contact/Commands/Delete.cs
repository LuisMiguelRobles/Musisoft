﻿namespace Application.Contact.Commands
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Errors;
    using MediatR;
    using Persistence;
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                    throw new RestException(HttpStatusCode.NotFound, new { Company = "Not found" });

                _context.Remove(contact);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }

        }
    }
}
