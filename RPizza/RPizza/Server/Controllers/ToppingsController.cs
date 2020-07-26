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
    [Route("toppings")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly RPizzaContext Context;

        public ToppingsController(RPizzaContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetTopping()
        {
            return await Context.Toppings
                .OrderByDescending(t => t.Name).ToListAsync();
        }
    }
}
