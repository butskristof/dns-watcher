using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DnsWatcher.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DnsWatcher.API.Middlewares
{
	internal class ErrorHandlingMiddleware
	{
		private readonly ILogger<ErrorHandlingMiddleware> _logger;
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next,
			ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
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

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(
				$"Error while processing request {context.Request.Method} {context.Request.Path}: {ex.Message}");
			if (ex is AutoMapperMappingException mappingException && mappingException.InnerException != null)
				ex = mappingException.InnerException;
			var code = ex switch
			{
				ArgumentException _ => HttpStatusCode.BadRequest,
				BadDataException _ => HttpStatusCode.BadRequest,
				DataChangedException _ => HttpStatusCode.BadRequest,
				ForbiddenException _ => HttpStatusCode.Forbidden,
				InfrastructureException _ => HttpStatusCode.InternalServerError,
				InvalidDataException _ => HttpStatusCode.BadRequest,
				NotFoundException _ => HttpStatusCode.NotFound,
				UnauthorizedException _ => HttpStatusCode.Unauthorized,
				_ => HttpStatusCode.InternalServerError
			};

			var result = JsonSerializer.Serialize(new {error = ex.Message});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int) code;
			return context.Response.WriteAsync(result);
		}
	}
}