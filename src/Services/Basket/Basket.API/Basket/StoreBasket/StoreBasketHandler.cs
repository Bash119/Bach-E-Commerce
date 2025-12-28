


using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null!");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}


public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discount) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        //TODO : communicate with other microservices if needed (e.g., to validate products, apply discounts, etc.)

       await DeductDiscount(cart,cancellationToken);



        //TODO: Store the basket in the database
        //TODO: Update cache

        await repository.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart,CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var discountResponse = await discount.GetDiscountAsync(discountRequest, cancellationToken: cancellationToken);
            item.Price -= discountResponse.Amount;
        }
    }
}
