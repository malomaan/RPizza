using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPizza.Server.Helpers
{
    public static class User
    {

        public static  string GetUserId(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Name)?.Value;
        }

    }
}
