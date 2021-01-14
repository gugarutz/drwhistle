using AutoMapper;
using DrWhistle.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DrWhistle.Application.Cases.Queries.GetCases
{
    public class GetCasesQuery : IRequest<CasesVM>
    {
    }

    public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, CasesVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCasesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CasesVM> Handle(GetCasesQuery request, CancellationToken cancellationToken)
        {
            return new CasesVM
            {
                Cases = new List<CaseDto>()
            };
        }
    }
}
