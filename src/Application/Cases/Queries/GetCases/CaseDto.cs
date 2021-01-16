using System.Collections.Generic;
using DrWhistle.Application.Common.Mappings;
using DrWhistle.Domain.Entities;

namespace DrWhistle.Application.Cases.Queries.GetCases
{
    public class CaseDto : IMapFrom<Case>
    {
        public CaseDto()
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public IList<Message> Messages { get; set; }
    }
}