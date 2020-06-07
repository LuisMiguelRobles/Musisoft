namespace Application.Campaign.Queries
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
        public class Query : IRequest<Campaign>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Campaign>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Campaign> Handle(Query request, CancellationToken cancellationToken)
            {
                var campaign = await _context.Campaigns.FindAsync(request.Id);

                if (campaign == null)
                    throw new RestException(HttpStatusCode.NotFound, new { campaign = "Not found" });

                return campaign;
            }
        }
    }
}
