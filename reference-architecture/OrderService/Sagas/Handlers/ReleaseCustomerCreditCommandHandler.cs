using EventDriven.EventBus.Abstractions;
using EventDriven.EventBus.Dapr;
using EventDriven.Sagas.Abstractions.Handlers;
using Integration.Events;
using Integration.Models;
using OrderService.Sagas.Commands;

namespace OrderService.Sagas.Handlers;

public class ReleaseCustomerCreditCommandHandler :
    ResultDispatchingSagaCommandHandler<CreateOrderSaga, ReleaseCustomerCredit, CustomerCreditReleaseResponse>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ReleaseCustomerCreditCommandHandler> _logger;

    public ReleaseCustomerCreditCommandHandler(
        IEventBus eventBus,
        ILogger<ReleaseCustomerCreditCommandHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public override async Task HandleCommandAsync(ReleaseCustomerCredit command)
    {
        _logger.LogInformation("Handling command: {CommandName}", nameof(ReleaseCustomerCredit));
        
        try
        {
            _logger.LogInformation("Publishing event: {EventName}", $"v1.{nameof(ReleaseCustomerCredit)}");
            await _eventBus.PublishAsync(
                new CustomerCreditReleaseRequested(
                    new CustomerCreditReleaseRequest(command.CustomerId, command.CreditReleased)),
                null, "v1");
        }
        catch (SchemaValidationException e)
        {
            _logger.LogError("{Message}", e.Message);
            await DispatchCommandResultAsync(new CustomerCreditReleaseResponse(
                command.CustomerId, command.CreditReleased,
                0, false), true);
        }
    }
}