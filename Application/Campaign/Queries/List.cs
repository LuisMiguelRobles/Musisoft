namespace Application.Campaign.Queries
{
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class List
    {
        public class Query: IRequest<List<Campaign>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Campaign>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Campaign>> Handle(Query request, CancellationToken cancellationToken)
            {
                var campaigns = _context.Campaigns.ToListAsync(cancellationToken);
                return await campaigns;
            }
        }
    }
}
