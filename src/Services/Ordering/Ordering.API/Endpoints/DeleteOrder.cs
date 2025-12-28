using Carter;
using Mapster;
using MediatR;

namespace Ordering.API.Endpoints;

//public record DeleteOrderRequest(Guid OrderId);

public record DeleteOrderResponse(bool Success);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var command = new Ordering.Application.Orders.Commands.DeleteOrder.DeleteOrderCommand(Id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        }).WithName("DeleteOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");
    }
}
