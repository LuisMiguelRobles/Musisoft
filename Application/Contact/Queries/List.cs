using System;
using System.Linq;

namespace Application.Contact.Queries
{
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class List 
    {
        public class Query : IRequest<List<Contact>>
        {
            public Guid CompanyId { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<Contact>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Contact>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Contacts.AsQueryable();
                var contacts = queryable.Where(x => x.CompanyId == request.CompanyId).ToListAsync(cancellationToken);
                return await contacts;
            }
        }
    }

    
}
