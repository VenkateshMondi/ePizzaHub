using ePizzaHub.Core.Contracts;
using ePizzaHub.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<int> GetCartItemsCountAsync(Guid cartId)
        {
            return await _cartRepository.GetCartItemQuantityAsync(cartId);
        }
    }
}
