using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DrWhistle.Application.Cases.EventHandlers
{
    public class CaseCreatedEventHandler : INotificationHandler<DomainEventNotification<CaseCreatedEvent>>
    {
        private readonly ILogger<CaseCompletedEventHandler> _logger;

        public CaseCreatedEventHandler(ILogger<CaseCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CaseCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("DrWhistle Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
