using System;
using System.Linq;
using Application.Interfaces;

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
        }
        public class Handler : IRequestHandler<Query, List<Contact>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<List<Contact>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Contacts.AsQueryable();
                var contacts = queryable.Where(x => x.User.UserName == _userAccessor.GetCurrentUsername()).ToListAsync(cancellationToken);
                return await contacts;
            }
        }
    }

    
}
