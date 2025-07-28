using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(PB655Context dbContext) : base(dbContext)
        {
        }
    }
}
