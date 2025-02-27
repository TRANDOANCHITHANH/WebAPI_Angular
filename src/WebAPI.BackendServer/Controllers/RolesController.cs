using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ViewModels.Systems;

namespace WebAPI.BackendServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public RolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		//URL: GET: http://localhost:7040/api/roles
		[HttpGet]
		public async Task<IActionResult> GetAllRoles()
		{
			var roles = await _roleManager.Roles.Select(r => new RoleVM()
			{
				Id = r.Id,
				Name = r.Name
			}).ToListAsync();
			return Ok(roles);
		}

		//URL: GET: http://localhost:7040/api/roles/?filter={filter}&pageIndex=1&pageSize=10
		[HttpGet("paged")]
		public async Task<IActionResult> GetRoles(string filter, int pageIndex, int pageSize)
		{
			var query = _roleManager.Roles;
			if (!string.IsNullOrEmpty(filter))
			{
				query = query.Where(x => x.Id.Contains(filter) || x.Name.Contains(filter));
			}
			var totalRecords = await query.CountAsync();
			var item = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new RoleVM
			{
				Id = x.Id,
				Name = x.Name
			}).ToListAsync();
			var pagigation = new Pagination<RoleVM>
			{
				Items = item,
				TotalRecord = totalRecords
			};
			return Ok(pagigation);
		}

		//URL: POST: http://localhost:7040/api/roles
		[HttpPost]
		public async Task<IActionResult> PostRole(RoleVM roleVM)
		{
			var role = new IdentityRole
			{
				Id = roleVM.Id,
				Name = roleVM.Name,
				NormalizedName = roleVM.Name.ToUpper()
			};
			var result = await _roleManager.CreateAsync(role);
			if (result.Succeeded)
			{
				return CreatedAtAction(nameof(GetById), new { id = role.Id }, roleVM);
			}
			else
			{
				return BadRequest(result.Errors);
			}

		}
		//URL: GET: http://localhost:7040/api/roles/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}
			var roleVM = new RoleVM
			{
				Id = role.Id,
				Name = role.Name
			};
			return Ok(roleVM);
		}
		//URL: PUT: http://localhost:7040/api/roles/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleVM roleVM)
		{
			if (id != roleVM.Id)
			{
				return BadRequest();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}
			role.Name = roleVM.Name;
			role.NormalizedName = roleVM.Name.ToUpper();
			var result = await _roleManager.UpdateAsync(role);
			if (result.Succeeded)
			{
				return NoContent();
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}
		//URL: DELETE: http://localhost:7040/api/roles/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRole(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}
			var result = await _roleManager.DeleteAsync(role);
			if (result.Succeeded)
			{
				var roleVM = new RoleVM
				{
					Id = role.Id,
					Name = role.Name
				};
				return Ok(roleVM);
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}
	}
}