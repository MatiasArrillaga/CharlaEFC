//using Algoritmo.Microservices.Shared.Domain.SeedWork;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Algoritmo.CharlaEFC.API.Services
//{
//    public class AuthenticatedUserService : IAuthenticatedUserService
//    {
//        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
//        {
//            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
//        }

//        public string UserId { get; }
//    }
//}
