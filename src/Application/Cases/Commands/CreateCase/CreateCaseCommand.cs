using System.Threading;
using System.Threading.Tasks;
using DrWhistle.Application.Cases.Queries.GetCases;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Domain.Entities;
using MediatR;

namespace DrWhistle.Application.Cases.Commands.CreateCase
{
    public class CreateCaseCommand : IRequest<int>
    {
        public CreateCaseCommand(CaseDto caseDto)
        {
            this.CaseDto = caseDto;
        }

        public CaseDto CaseDto { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<>")]
    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCaseCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Case
            {
                Title = request.CaseDto.Title
            };

            context.Cases.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}