using Bogus;
using InventoryService.Application.Query;
using InventoryService.Application.ViewModel;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Application.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
    {
        public async Task<List<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var i = 0;
            var faker = new Faker<ProductViewModel>()
                .RuleFor(p => p.Id, f => ++i)
                .RuleFor(p=> p.Title, f=>f.Commerce.Product())
                .RuleFor(p=> p.Price, f=>f.Commerce.Price());
            var products = faker.Generate(20);
            return products;
        }
    }
}
