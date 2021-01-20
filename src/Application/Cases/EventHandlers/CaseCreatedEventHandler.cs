using System.Threading;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrWhistle.Application.Cases.EventHandlers
{
    public class CaseCreatedEventHandler : INotificationHandler<DomainEventNotification<CaseCreatedEvent>>
    {
        private readonly ILogger<CaseCompletedEventHandler> logger;

        public CaseCreatedEventHandler(ILogger<CaseCompletedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(DomainEventNotification<CaseCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            logger.LogInformation("DrWhistle Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}