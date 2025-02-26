using Microsoft.AspNetCore.Identity;
using WebAPI.BackendServer.Data.Entities;

namespace WebAPI.BackendServer.Data
{
	public class DbInitial
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly string AdminRoleName = "Admin";
		private readonly string UserRoleName = "Member";

		public DbInitial(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task SeedData()
		{
			#region Quyền
			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole
				{
					Id = AdminRoleName,
					Name = AdminRoleName,
					NormalizedName = AdminRoleName.ToUpper()
				});
				await _roleManager.CreateAsync(new IdentityRole
				{
					Id = UserRoleName,
					Name = UserRoleName,
					NormalizedName = UserRoleName.ToUpper()
				});
			}
			#endregion

			#region User
			if (!_userManager.Users.Any())
			{
				var result = await _userManager.CreateAsync(new User
				{
					Id = Guid.NewGuid().ToString(),
					FirstName = "Admin",
					UserName = "admin",
					LastName = "1",
					Email = "tranchithanh0404@gmail.com",
					LockoutEnabled = false,

				}, "Admin@123");
				if (result.Succeeded)
				{
					var user = await _userManager.FindByNameAsync("admin");
					await _userManager.AddToRoleAsync(user, AdminRoleName);
				}
			}
			#endregion

			#region Function
			if (!_context.Functions.Any())
			{
				_context.Functions.AddRange(new List<Function>
				{
					new Function {Id = "DASHBOARD", Name = "Thống kê", ParentId = null, SortOrder = 1, Url = "/dashboard"},
					new Function {Id = "CATEGORY", Name = "Category", ParentId = null, SortOrder = 2, Url = "/category"},
					new Function {Id = "KNOWLEDGEBASE", Name = "Knowledge Base", ParentId = null, SortOrder = 3, Url = "/knowledge-base"},
					new Function {Id = "REPORT", Name = "Report", ParentId = null, SortOrder = 4, Url = "/report" },
					new Function {Id = "USER", Name = "User", ParentId = null, SortOrder = 5, Url = "/user"},
					new Function {Id = "ROLE", Name = "Role", ParentId = null, SortOrder = 6, Url = "/role"},
					new Function {Id = "FUNCTION", Name = "Function", ParentId = null, SortOrder = 7, Url = "/function" },
					new Function {Id = "COMMAND", Name = "Command", ParentId = null, SortOrder = 8, Url = "/command" },
					new Function {Id = "SETTING", Name = "Setting", ParentId = null, SortOrder = 9, Url = "/setting"},
				});
				await _context.SaveChangesAsync();
			}
			if (!_context.Commands.Any())
			{
				_context.Commands.AddRange(new List<Command>()
				{
					new Command (){Id = "VIEW", Name = "Xem" },
					new Command (){Id = "CREATE", Name = "Thêm" },
					new Command (){Id = "UPDATE", Name = "Sửa" },
					new Command (){Id = "DELETE", Name = "Xóa" },
					new Command (){Id = "APROVE", Name = "Duyệt" },
				});
				await _context.SaveChangesAsync();
				#endregion	Function
				var functionIds = _context.Functions;
				if (!_context.CommandInFunctions.Any())
				{
					foreach (var function in functionIds)
					{
						var createAction = new CommandInFunctions()
						{
							CommandId = "CREATE",
							FunctionId = function.Id
						};
						_context.CommandInFunctions.Add(createAction);
						var readAction = new CommandInFunctions()
						{
							CommandId = "VIEW",
							FunctionId = function.Id
						};
						_context.CommandInFunctions.Add(readAction);
						var updateAction = new CommandInFunctions()
						{
							CommandId = "UPDATE",
							FunctionId = function.Id
						};
						_context.CommandInFunctions.Add(updateAction);
						var deleteAction = new CommandInFunctions()
						{
							CommandId = "DELETE",
							FunctionId = function.Id
						};
						_context.CommandInFunctions.Add(deleteAction);
						var aproveAction = new CommandInFunctions()
						{
							CommandId = "APROVE",
							FunctionId = function.Id
						};
						_context.CommandInFunctions.Add(aproveAction);
					}
				}
				if (!_context.Permissions.Any())
				{
					var adminRole = await _roleManager.FindByNameAsync(AdminRoleName);
					foreach (var function in functionIds)
					{
						_context.Permissions.Add(new Permission(function.Id, adminRole.Id, "CREATE"));
						_context.Permissions.Add(new Permission(function.Id, adminRole.Id, "VIEW"));
						_context.Permissions.Add(new Permission(function.Id, adminRole.Id, "UPDATE"));
						_context.Permissions.Add(new Permission(function.Id, adminRole.Id, "DELETE"));
						_context.Permissions.Add(new Permission(function.Id, adminRole.Id, "APROVE"));

					}
				}
				await _context.SaveChangesAsync();
			}
		}
	}
}
