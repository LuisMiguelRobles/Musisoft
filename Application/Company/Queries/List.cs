namespace Application.Company.Queries
{
    using Interfaces;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class List
    {
        public class Query : IRequest<List<Company>> { }

        public class Handler : IRequestHandler<Query, List<Company>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<List<Company>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Companies.AsQueryable();
                var companies = queryable.Where(x => x.User.UserName == _userAccessor.GetCurrentUsername()).ToListAsync(cancellationToken);
                return await companies;
            }
        }
    }
}
