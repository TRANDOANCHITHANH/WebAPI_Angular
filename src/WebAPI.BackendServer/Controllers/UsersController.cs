using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.BackendServer.Data.Entities;
using WebAPI.ViewModels.Systems;

namespace WebAPI.BackendServer.Controllers
{

	public class UsersController : BaseController
	{
		private readonly UserManager<User> _userManager;
		public UsersController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		//URL: GET: http://localhost:7040/api/users
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userManager.Users.Select(u => new UserVM()
			{
				Id = u.Id,
				UserName = u.UserName,
				Email = u.Email,
				Dob = u.Dob,
				FirstName = u.FirstName,
				LastName = u.LastName,
			}).ToListAsync();
			return Ok(users);
		}
		//URL: GET: http://localhost:7040/api/users/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var userVM = new UserVM()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = user.UserName,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Dob = user.Dob,
				PhoneNumber = user.PhoneNumber,
			};
			return Ok(userVM);
		}
		//URL: POST: http://localhost:7040/api/users
		[HttpPost]
		public async Task<IActionResult> PostUser(UserCreateRequest request)
		{
			var newUser = new User
			{
				Id = Guid.NewGuid().ToString(),
				UserName = request.UserName,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Dob = request.Dob,
				PhoneNumber = request.PhoneNumber,
			};
			var result = await _userManager.CreateAsync(newUser, request.Password);
			if (result.Succeeded)
			{
				return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, request);
			}
			return BadRequest(result.Errors);
		}

		//URL: PUT: http://localhost:7040/api/users
		[HttpPut]
		public async Task<IActionResult> PutUser(string id, [FromBody] UserCreateRequest request)
		{
			var userUpdate = await _userManager.FindByIdAsync(id);
			if (userUpdate == null)
			{
				return NotFound();
			}
			userUpdate.FirstName = request.FirstName;
			userUpdate.Email = request.Email;
			userUpdate.LastName = request.LastName;
			userUpdate.Dob = request.Dob;
			var result = await _userManager.UpdateAsync(userUpdate);
			if (result.Succeeded)
			{
				return NoContent();
			}
			return BadRequest(result.Errors);
		}
		//URL: DELETE: http://localhost:7040/api/users/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRole(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				var userVM = new UserVM()
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Dob = user.Dob,
					PhoneNumber = user.PhoneNumber,
				};
				return Ok(userVM);
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}
	}
}
