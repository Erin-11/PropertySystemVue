using KnightFrank.BAL.Extensions;
using KnightFrank.MemfusWongData.Api.Exceptions;
using KnightFrank.MemfusWongData.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace KnightFrank.MemfusWongData.Api.Filters
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptoinFilter : IExceptionFilter, IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Performs while an exception arises.
        /// </summary>
        /// <param name="context"><see cref="ExceptionContext"/> instance.</param>
        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse() { Message = context.Exception.GetExceptionErrorMessage() };
            response.StackTrace = context.Exception.StackTrace;

            context.Result = new ObjectResult(response)
            {
                StatusCode = GetHttpStatusCode(context.Exception),
                DeclaredType = typeof(ErrorResponse)
            };
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }

        private static int GetHttpStatusCode(Exception ex)
        {
            if (ex is HttpResponseException)
            {
                return (int)(ex as HttpResponseException).HttpStatusCode;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
