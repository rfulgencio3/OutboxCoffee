using OutboxCoffee.Worker.Steps;
using WorkflowCore.Interface;

namespace OutboxCoffee.Worker.Processors;

public class OutboxDispatchWorkflow : IWorkflow
{
    public string Id => "OutboxDispatchWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith<ReadOutboxStep>()
            .Then<PublishEventStep>()
            .Then<MarkAsProcessedStep>();
    }
}