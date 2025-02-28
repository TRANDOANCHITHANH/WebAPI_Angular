using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();
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
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
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
