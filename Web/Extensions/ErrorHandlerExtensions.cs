using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions;

public static class ErrorHandlerExtensions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = contextFeature.Error switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    NullRequestException => (int)HttpStatusCode.NotFound,
                    InternalServerException => (int)HttpStatusCode.InternalServerError,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    DbUpdateException => (int)HttpStatusCode.Conflict,
                };

                object errorResponse;

                if (contextFeature.Error is BadRequestException badRequestEx)
                {
                    errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = badRequestEx.Message,
                        errors = badRequestEx.Errors ?? new[] { badRequestEx.Error }
                    };
                } else if (contextFeature.Error is NullRequestException nullRequest)
                {
                    errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = nullRequest.Message,
                    };
                } else if (contextFeature.Error is InternalServerException serverException)
                {
                    errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = serverException.Message,
                    };
                } else if (contextFeature.Error is ApplicationException appEx)
                {
                    errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = appEx.Message
                    };
                }
                else
                {
                    errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message
                    };
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}