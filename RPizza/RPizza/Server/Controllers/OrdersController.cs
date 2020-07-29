using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPizza.Server.Models;
using RPizza.Shared;

namespace RPizza.Server.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly RPizzaContext Context;

        public OrdersController(RPizzaContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            order.CreatedTime = DateTime.Now;
            order.DeliveryLocation =
                new LatLong(19.043679206924864, -98.19811254438645);
            foreach (var pizza in order.Pizzas)
            {
                pizza.SpecialId = pizza.SpecialId;
                pizza.Special = null;
                foreach (var topping in pizza.Toppings)
                {
                    topping.ToppingId = topping.Topping.Id;
                    topping.Topping = null;
                }
            }
            Context.Orders.Attach(order);
            await Context.SaveChangesAsync();
            return order.OrderId;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            var Orders = await Context.Orders
                .Include(o => o.DeliveryLocation)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .OrderByDescending(o => o.CreatedTime).ToListAsync();
            return Orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
        }
      
    }
}
