
namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        //TODO: Delete the basket from the database
        //session.Delete<Product>(command.userName)

        await repository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
