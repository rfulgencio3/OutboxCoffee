using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OutboxCoffee.Worker.Processors;
using OutboxCoffee.Worker.Steps;
using WorkflowCore.Interface;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddLogging();
        services.AddWorkflow();

        services.AddTransient<ReadOutboxStep>();
        services.AddTransient<PublishEventStep>();
        services.AddTransient<MarkAsProcessedStep>();

        services.AddTransient<IWorkflow, OutboxDispatchWorkflow>();
    })
    .Build();

var workflowHost = host.Services.GetRequiredService<IWorkflowHost>();
workflowHost.RegisterWorkflow<OutboxDispatchWorkflow>();
workflowHost.Start();

Console.WriteLine("Outbox Worker running. Press Enter to exit...");
Console.ReadLine();
workflowHost.Stop();
