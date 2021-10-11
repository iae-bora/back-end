using IaeBoraLibrary.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IaeBoraLibrary.Service;
using Newtonsoft.Json;
using System.Net;
using System;

namespace IaeBoraAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string responseMessage = string.Empty;
            if (exception is ExceptionBase)
                responseMessage = exception.Message;
            else
            {
                responseMessage = string.Concat("Erro não mapeado pela API. Erro: " + exception.Message);
                LogService.Write(exception);
            }
            var resultjson = JsonConvert.SerializeObject(new { message = responseMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            return context.Response.WriteAsync(resultjson);
        }
    }
}
