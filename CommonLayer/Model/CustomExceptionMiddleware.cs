using CommonLayer.Model.GlobalCustomException;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLayer.Model
{

    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case CustomException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case InvalidOperationException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AccessViolationException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case SqlException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                   

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { Status = false, message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }

}
