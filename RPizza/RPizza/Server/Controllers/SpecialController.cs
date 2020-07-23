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
   
    
    [Route("special")]
    [ApiController]
    public class SpecialController : ControllerBase
    {

        private readonly RPizzaContext Context;

        public SpecialController(RPizzaContext context)
        {
            Context = context;
        }



        /// <summary>
        /// Metodo Get que obtiene el listado de Pizzas de la tabla Special
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecial()
        {
            return await Context.Special
                .OrderByDescending(s => s.BasePrice).ToListAsync();
        } 



    }




}
