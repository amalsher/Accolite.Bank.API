using System.Net;
using Accolite.Bank.Services.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Accolite.Bank.API.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    context.Response.ContentType = "text/plain";

                    if (contextFeature.Error is AccountException exception)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        await context.Response.WriteAsync(exception.Message);

                        return;
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                await context.Response.WriteAsync("Internal Server Error");
            });
        });
    }
}
