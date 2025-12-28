namespace Basket.API.Basket.GetBasket;


//public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart ShoppingCart);
public class GetBasketEndPoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("Get Basket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket");
    }
}
