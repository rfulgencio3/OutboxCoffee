using OutboxCoffee.Core.Domain.Entities;
using OutboxCoffee.Core.Domain.Interfaces;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OutboxCoffee.Worker.Steps;

public class ReadOutboxStep : StepBody
{
    private readonly IOutboxRepository _repository;
    public List<OutboxMessage> Messages { get; private set; } = new();

    public ReadOutboxStep(IOutboxRepository repository)
    {
        _repository = repository;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Messages = _repository.GetUnprocessedMessagesAsync().Result;
        return ExecutionResult.Next();
    }
}