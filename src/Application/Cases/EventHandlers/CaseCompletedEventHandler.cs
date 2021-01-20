using System.Threading;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrWhistle.Application.Cases.EventHandlers
{
    public class CaseCompletedEventHandler : INotificationHandler<DomainEventNotification<CaseCompletedEvent>>
    {
        private readonly ILogger<CaseCompletedEventHandler> logger;

        public CaseCompletedEventHandler(ILogger<CaseCompletedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(DomainEventNotification<CaseCompletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            logger.LogInformation("DrWhistle Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}