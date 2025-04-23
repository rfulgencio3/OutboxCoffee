using Microsoft.EntityFrameworkCore.Storage;
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
        var messages = _repository.GetUnprocessedMessagesAsync().Result;

        foreach (var msg in messages)
        {
            _repository.MarkAsProcessedAsync(msg.Id).Wait();
        }

        return ExecutionResult.Next();
    }
}