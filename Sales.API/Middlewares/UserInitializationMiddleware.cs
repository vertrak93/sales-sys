using Sales.Data.UnitOfWork;
using System.Security.Claims;

namespace Sales.API.Middlewares
{
    public class UserInitializationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInitializationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var unitOfWork = context.RequestServices.GetRequiredService<IUnitOfWork>();
            //if (context.User.Identity.IsAuthenticated)
            //{
            //    var claimName = context.User.FindFirst(ClaimTypes.Name);

            //    if (claimName != null)
            //    {
            //        unitOfWork.UserLogged = claimName.Value;
            //    }
            //}

            await _next(context);
        }
    }
}
