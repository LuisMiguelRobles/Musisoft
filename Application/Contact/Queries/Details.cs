namespace Application.Contact.Queries
{
    using Application.Errors;
    using Domain;
    using MediatR;
    using Persistence;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Details
    {
        public class Query: IRequest<Contact>
        {
            public Guid Id { get; set; }
        }

        public class Handler: IRequestHandler<Query, Contact>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Contact> Handle(Query request, CancellationToken cancellationToken)
            {
                var contact = await _context.Contacts.FindAsync(request.Id);

                if (contact == null)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not found" });

                return contact;
            }
        }
    }
}
