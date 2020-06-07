
namespace Application.CompanyContacts.Queries
{
    using System.Collections.Generic;
    using Application.Interfaces;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Threading;
    using System.Threading.Tasks;

    public class List
    {
        public class Query : IRequest<List<CompanyContacts>> { }

        public class Handler : IRequestHandler<Query, List<CompanyContacts>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<CompanyContacts>> Handle(Query request, CancellationToken cancellationToken)
            {
                var companyContacts = _context.CompanyContacts.ToListAsync(cancellationToken);
                return await companyContacts;
            }
        }
    }
}
