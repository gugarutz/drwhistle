using DrWhistle.Application.Cases.Queries.GetCases;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Domain.Entities;
using DrWhistle.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DrWhistle.Application.Cases.Commands.CreateCase
{
    public class CreateCaseCommand : IRequest<int>
    {
        public CaseDto caseDto;
        public CreateCaseCommand(CaseDto _caseDto)
        {
            caseDto = _caseDto;
        }
    }
    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCaseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Case
            {
                Title = request.caseDto.Title
            };

            _context.Cases.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
