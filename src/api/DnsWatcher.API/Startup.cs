using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DnsWatcher.API.Middlewares;
using DnsWatcher.Application;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common;
using DnsWatcher.Common.Configuration;
using DnsWatcher.Common.Constants;
using DnsWatcher.Infrastructure;
using DnsWatcher.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DnsWatcher.API
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			AddOptions(services);

			services
				.AddCommon()
				.AddApplication()
				.AddInfrastructure()
				.AddPersistence(_configuration)
				.AddApi();

			AddAuthentication(services);

			services.AddCors();

			services
				.AddControllers()
				.AddFluentValidation(fvc => { fvc.RegisterValidatorsFromAssemblyContaining<IAuthService>(); });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

			app.UseCors(builder =>
				{
					builder.AllowAnyHeader();
					builder.AllowAnyMethod();
					builder.AllowAnyOrigin();
				}
			);

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseMiddleware(typeof(ErrorHandlingMiddleware));
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
			});
		}

		#region options

		private void AddOptions(IServiceCollection services)
		{
			#region JWT

			services
				.Configure<JwtOptions>(
					_configuration.GetSection(ConfigurationConstants.Jwt)
				);
			services
				.AddSingleton<IJwtOptions>(
					sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value
				);

			#endregion

			#region Encryption

			services
				.Configure<EncryptionOptions>(
					_configuration.GetSection(ConfigurationConstants.Encryption)
				);
			services
				.AddSingleton<IEncryptionOptions>(
					sp => sp.GetRequiredService<IOptions<EncryptionOptions>>().Value
				);

			#endregion

			#region Application

			services
				.Configure<ApplicationOptions>(
					_configuration.GetSection(ConfigurationConstants.Application)
				);
			services
				.AddSingleton<IApplicationOptions>(
					sp => sp.GetRequiredService<IOptions<ApplicationOptions>>().Value
				);

			#endregion
		}

		#endregion

		#region authentication

		private void AddAuthentication(IServiceCollection services)
		{
			var jwtOptions = _configuration
				.GetSection(ConfigurationConstants.Jwt)
				.Get<JwtOptions>();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
			services
				.AddAuthentication(options =>
				{
					//Set default Authentication Schema as Bearer
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;
					cfg.TokenValidationParameters =
						new TokenValidationParameters
						{
							ValidIssuer = jwtOptions.Issuer,
							ValidAudience = jwtOptions.Issuer,
							IssuerSigningKey =
								new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
							ClockSkew = TimeSpan.Zero // remove delay of token when expire
						};
				});
		}

		#endregion

		#region construction

		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		#endregion
	}
}