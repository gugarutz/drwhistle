using DrWhistle.Application.Common.Mappings;
using DrWhistle.Domain.Entities;
using System.Collections.Generic;

namespace DrWhistle.Application.Cases.Queries.GetCases
{
    public class CaseDto : IMapFrom<Case>
    {
        public CaseDto()
        {
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
