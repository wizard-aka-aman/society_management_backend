using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Society_Management_System.Model;

namespace Society_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration configuration; 
        private readonly SocietyContext _societyContext; 

        public AccountController(SocietyContext societyContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager , IConfiguration config)
        {
              _societyContext = societyContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            configuration = config; 
        }
         

        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]  Login LoginModel)
        { 
            var user= await _userManager.FindByNameAsync(LoginModel.UserName);
            //Users userTable =   _societyContext.Users.Where(e => e.Name == LoginModel.UserName);
            if (user == null) { 
                //return Ok("User Not Registered!");
                return NotFound("User Not Registered!");
            }
             

            if (!await _userManager.CheckPasswordAsync(user, LoginModel.Password))
            {
                //return Ok("Invalid Credentials!!");
                return BadRequest("Invalid Credentials!!");
            }
            Users userTable = _societyContext.Users.First(e => e.Name == LoginModel.UserName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub ,configuration["JwtConfig:Subject"]?? "" ),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() ),
                new Claim("UserName" , user.UserName.ToString() ?? ""),
                new Claim("Email" , user.Email.ToString()?? "" ),
                new Claim("Role" , userRoles[0].ToString()?? ""),
                new Claim("Society" , userTable.SocietyId.ToString()??""), 
            };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"] ));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["JwtConfig:Issuer"],
                configuration["JwtConfig:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signIn
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            

            var result = await _signInManager.PasswordSignInAsync(
                    user.UserName, LoginModel.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded) {
                return Ok(new { Token = tokenValue, User = user , userRoles = userRoles });
            }
            //return Ok("Login Failed ! Please try again");
            
            return BadRequest("Login Failed ! Please try again");
        }
        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Register registerModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            var usernameExist = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExist != null)
            {
                return BadRequest("User with this email already exists.");
            }

            if (usernameExist != null)
            {
                return BadRequest("Username is already taken. Please choose another.");
            }
            if (userExist == null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));

                if (await _roleManager.RoleExistsAsync("User"))
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                 
                if (result.Succeeded) {
                    var User = new Users
                    {
                        Email = registerModel.Email,
                        Name = registerModel.UserName,
                        Role = "User",
                        SocietyId = registerModel.SocietyId,
                        
                    };
                    _societyContext.Users.Add(User);
                    await _societyContext.SaveChangesAsync();

                    return Ok(user);
                }
                else
                {
                    //return Ok("Register Failed ! Please try again");
                    return BadRequest("Username can't contian spaces and special character");
                }
            }
            //return Ok("User Already Exist");
            return BadRequest("User Already Exist");
        }
        [HttpPost("registerAdmin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register registerModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            var usernameExist = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExist != null)
            {
                return BadRequest("User with this email already exists.");
            }

            if (usernameExist != null)
            {
                return BadRequest("Username is already taken. Please choose another.");
            }
            if (userExist == null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                if (await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                if (result.Succeeded)
                {
                    var User = new Users
                    {
                        Email = registerModel.Email,
                        Name = registerModel.UserName,
                        Role = "Admin",
                         SocietyId =registerModel.SocietyId,
                    };
                    _societyContext.Users.Add(User);
                    await _societyContext.SaveChangesAsync();

                    return Ok(user);
                }
                else
                {
                    //return Ok("Register Failed ! Please try again");
                    return BadRequest("Username can't contian spaces and special character");
                }
            }
            //return Ok("User Already Exist");
            return BadRequest("User Already Exist");
        }
        [HttpPost("registerSuperAdmin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] Register registerModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            var usernameExist = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExist != null)
            {
                return BadRequest("User with this email already exists.");
            }

            if (usernameExist != null)
            {
                return BadRequest("Username is already taken. Please choose another.");
            }
            if (userExist == null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                if (await _roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    await _userManager.AddToRoleAsync(user, "SuperAdmin");
                }

                if (result.Succeeded)
                {
                    var User = new Users
                    {
                        Email = registerModel.Email,
                        Name = registerModel.UserName,
                        Role = "SuperAdmin"

                    };
                    _societyContext.Users.Add(User);
                    await _societyContext.SaveChangesAsync();

                    return Ok(user);
                }
                else
                {
                    //return Ok("Register Failed ! Please try again");
                    return BadRequest("Username can't contian spaces and special character");
                }
            }
            //return Ok("User Already Exist");
            return BadRequest("User Already Exist");
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json("User Logout");
        }

        [HttpGet("getallusers/{id}")]
        public async Task<IActionResult> getallusers(int id)
        {

            //var items = await _userManager.Users.Select(e => e.UserName ).ToListAsync();
            var items = await _societyContext.Users.Where(e => e.SocietyId == id).Select(x => new { x.Name,x.Email, x.Role }).ToListAsync();
            return Ok(items);
        }
        [HttpGet("getallunassignedusers/{id}")]
        public async Task<IActionResult> getallunassignedusers(int id)
        {

            //var items = await _userManager.Users.Select(e => e.UserName ).ToListAsync();
            List<string> users = await _societyContext.Users.Where(e => e.Role != "SuperAdmin" && e.SocietyId == id).Select(s => s.Name).ToListAsync();
            List<string> flats= await _societyContext.Flats.Include(e => e.Users).Select(e => e.Users.Name).ToListAsync();
            List<string> unassignedUsers = new List<string>();
            for (int i = 0; i < users.Count; i++)
            {
                

                var user = flats.Contains(users[i]);
                    if(!user)
                unassignedUsers.Add(users[i]); 
            }
            return Ok(unassignedUsers);
        }

        [HttpDelete("DeleteUser/{name}")]
        public async Task<bool> DeleteUser(string name)
        {
            // Find the user by name
            var user = await _societyContext.Users.FirstOrDefaultAsync(e => e.Name == name);
            if (user == null)
            {
                return false;
            }

            // Check for foreign key dependency (assuming Flats table references Users by UserId)
            bool isReferencedInFlats = await _societyContext.Flats.Include(e => e.Users).AnyAsync(f => f.Users!=null && f.Users.UsersId == user.UsersId);
            if (isReferencedInFlats)
            {
                // User cannot be deleted due to foreign key constraints
                return false;
            }

            // Remove the user from Users table
            _societyContext.Users.Remove(user);
            await _societyContext.SaveChangesAsync();

            // Also remove from Identity (UserManager)
            var identityUser = await _userManager.FindByNameAsync(name);
            if (identityUser != null)
            {
                await _userManager.DeleteAsync(identityUser);
                await _societyContext.SaveChangesAsync();
            }
            List<Users> users = _societyContext.Users.Where(e => e.Role == "Admin" && e.SocietyId == user.SocietyId).ToList();
            if(users.Count != 0)
            {
                Society society =await _societyContext.Society.FirstOrDefaultAsync(e => e.SocietyId == users[0].SocietyId);
                society.Admin = users[0].Name;
                _societyContext.Society.Update(society);
                await _societyContext.SaveChangesAsync();
            }


            return true;
        }

        [HttpGet("GetUserDetail/{name}")]
        public  IActionResult GetUserDetail(string name)
        {
            var user = _societyContext.Users.Where(e => e.Name == name);
            if (user != null) { 
            var flat = _societyContext.Flats.Include(e => e.Users).Where(e => e.Users != null && e.Users.Name == name);
            return Ok(new {  flat ,user });
            }
            return Ok(user);
        }

        [HttpGet("GetUsernameWithNull")]
        public IActionResult GetUsernameWithNull()
        {
            var user = _societyContext.Users.Where(e => e.SocietyId == null && e.Role == "Admin");
            
            return Ok(user);
        }
        [HttpGet("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var user = _societyContext.Users.Where(e => e.Role == "Admin");

            return Ok(user);
        }
        [HttpGet("GetSocietyDetail/{id}")]
        public IActionResult GetSocietyDetail(int id)
        {
            var society = _societyContext.Society.Where(e => e.SocietyId == id);

            return Ok(society);
        }

        [HttpGet("GetAllAdminOfSociety/{id}")]
        public IActionResult GetAllAdminOfSociety(int id)
        {
            var user = _societyContext.Users.Where(e => e.Role == "Admin" && e.SocietyId == id);

            return Ok(user);
        }

    }
}
