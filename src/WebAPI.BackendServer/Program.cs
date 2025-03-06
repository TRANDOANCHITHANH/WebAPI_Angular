using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.BackendServer.Data;
using WebAPI.BackendServer.Data.Entities;
using WebAPI.BackendServer.IdentityServer;
using WebAPI.BackendServer.Services;
using WebAPI.ViewModels.Systems;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<RoleVMValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.OAuth2,
		Flows = new OpenApiOAuthFlows
		{
			Implicit = new OpenApiOAuthFlow
			{
				AuthorizationUrl = new Uri("https://localhost:7040/connect/authorize"),
				Scopes = new Dictionary<string, string>
				{
					{ "api.webapi", "Web API" }
				}
			}
		}
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new List<string>{ "api.webapi" }
		}
	});
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseFailureEvents = true;
	options.Events.RaiseSuccessEvents = true;
}).AddInMemoryApiResources(Config.Apis)
	.AddInMemoryClients(Config.Clients)
	.AddInMemoryIdentityResources(Config.Ids)
	.AddAspNetIdentity<User>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();
builder.Services.AddRazorPages(option =>
{
	option.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
	{
		foreach (var selector in model.Selectors)
		{
			var attributeRouteModel = selector.AttributeRouteModel;
			attributeRouteModel.Order = -1;
			attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
		}
	});
	option.Conventions.AuthorizeFolder("/Users");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
		c.OAuthClientId("swagger");
	});
}
app.UseIdentityServer();
app.UseHttpsRedirection();
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
/*app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	endpoints.MapRazorPages();
});*/
app.Run();
