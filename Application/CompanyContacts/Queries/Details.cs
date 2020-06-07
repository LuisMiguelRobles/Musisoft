namespace Application.CompanyContacts.Queries
{
    using MediatR;
    using System;
    using Application.Errors;
    using Domain;
    using Persistence;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Details
    {
        public class Query : IRequest<CompanyContacts>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CompanyContacts>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<CompanyContacts> Handle(Query request, CancellationToken cancellationToken)
            {
                var companyContacts = await _context.CompanyContacts.FindAsync(request.Id);

                if (companyContacts == null)
                    throw new RestException(HttpStatusCode.NotFound, new { CompanyContacts = "Not found" });

                return companyContacts;
            }
        }
    }
}
