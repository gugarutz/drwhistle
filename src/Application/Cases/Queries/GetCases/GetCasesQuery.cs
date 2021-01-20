using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DrWhistle.Application.Common.Interfaces;
using MediatR;

namespace DrWhistle.Application.Cases.Queries.GetCases
{
    public class GetCasesQuery : IRequest<CasesVM>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<>")]
    public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, CasesVM>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCasesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CasesVM> Handle(GetCasesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new CasesVM
            {
                Cases = new List<CaseDto>()
            });
        }
    }
}