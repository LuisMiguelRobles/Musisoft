namespace Application.Company.Queries
{
    using Domain;
    using Errors;
    using MediatR;
    using Persistence;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Details
    {
        public class Query: IRequest<Company>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Company>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Company> Handle(Query request, CancellationToken cancellationToken)
            {
                var company = await _context.Companies.FindAsync(request.Id);

                if (company == null)
                    throw new RestException(HttpStatusCode.NotFound, new { company = "Not found" });

                return company;
            }
        }
    }
}
