

using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;


public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);
public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // TODO : get basket from database
        // var basket = await _basketRepository.GetBasketAsync(query.UserName);
        var basket= await repository.GetBasket(query.UserName);
        return new GetBasketResult(basket!);
    }
}
