using OutboxCoffee.Core.Domain.Interfaces;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OutboxCoffee.Worker.Steps;

public class PublishEventStep : StepBody
{
    private readonly IEventPublisher _publisher;

    public PublishEventStep(IEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        foreach (var msg in ReadOutboxStep.Messages)
        {
            _publisher.PublishAsync(msg.Type, msg.Payload).Wait();
        }

        return ExecutionResult.Next();
    }
}
