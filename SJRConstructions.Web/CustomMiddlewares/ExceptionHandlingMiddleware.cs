﻿using System.Net;
using System.Text.Json;
//using ExceptionHandling.Models.Responses;
using SJRConstructions.Web.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SJRConstructions.Web.CustomMiddlewares
{
	public class ExceptionHandlingMiddleware
	{

		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = context.Response;

		
			switch (exception)
			{
				case ApplicationException ex:
					if (ex.Message.Contains("Invalid Token"))
					{
						response.StatusCode = (int)HttpStatusCode.Forbidden;						
						break;
					}
					response.StatusCode = (int)HttpStatusCode.BadRequest;					
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;					
					break;

			}
			_logger.LogError(exception.Message); 
            context.Response.Redirect($"/Home/Error");
        }

	}
}
