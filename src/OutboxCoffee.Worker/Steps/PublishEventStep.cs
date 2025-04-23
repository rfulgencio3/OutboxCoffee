using OutboxCoffee.Core.Domain.Interfaces;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OutboxCoffee.Worker.Steps;

public class PublishEventStep : StepBody
{
    private readonly IEventPublisher _publisher;
    private readonly IOutboxRepository _repository;

    public PublishEventStep(IEventPublisher publisher, IOutboxRepository repository)
    {
        _publisher = publisher;
        _repository = repository;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var messages = _repository.GetUnprocessedMessagesAsync().Result;

        foreach (var msg in messages)
        {
            _publisher.PublishAsync(msg.Type, msg.Payload).Wait();
        }

        return ExecutionResult.Next();
    }
}