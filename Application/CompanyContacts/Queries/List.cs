using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CompanyContacts.Queries
{
    using Application.Interfaces;
    using Domain;
    using MediatR;
    using Persistence;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class List
    {
        public class Query : IRequest<List<CompanyContacts>> { }

        public class Handler : IRequestHandler<Query, List<CompanyContacts>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<List<CompanyContacts>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.CompanyContacts.AsQueryable();
                var companyContacts = queryable.Where(x => x.User.UserName == _userAccessor.GetCurrentUsername()).ToListAsync(cancellationToken);
                return await companyContacts;
            }
        }
    }
}
