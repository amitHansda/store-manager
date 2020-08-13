using InventoryService.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Query
{
    public class GetProductsQuery :IRequest<List<ProductViewModel>>
    {

    }


}
