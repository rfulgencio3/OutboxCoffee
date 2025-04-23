using OutboxCoffee.Core.Domain.Interfaces;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OutboxCoffee.Worker.Steps;

public class MarkAsProcessedStep : StepBody
{
    private readonly IOutboxRepository _repository;

    public MarkAsProcessedStep(IOutboxRepository repository)
    {
        _repository = repository;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        foreach (var msg in ReadOutboxStep.Messages)
        {
            _repository.MarkAsProcessedAsync(msg.Id).Wait();
        }

        return ExecutionResult.Next();
    }
}